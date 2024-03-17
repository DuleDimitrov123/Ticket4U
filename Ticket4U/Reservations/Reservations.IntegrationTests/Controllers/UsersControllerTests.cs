using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Helpers;
using Shared.Domain.Events;
using Shouldly;
using System.Net;
using Xunit.Abstractions;

namespace Reservations.IntegrationTests.Controllers;

public class UsersControllerTests : UsersControllerHelper
{
    public UsersControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
    }

    [Fact]
    public async Task CreateUserSuccessfully()
    {
        var createUserRequest = new CreatedUserEvent("testuser@gmail.com", "testuser", Guid.NewGuid());
        var (statusCode, result) = await CreateUser<Guid>(createUserRequest, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBe(Guid.Empty);
    }
}
