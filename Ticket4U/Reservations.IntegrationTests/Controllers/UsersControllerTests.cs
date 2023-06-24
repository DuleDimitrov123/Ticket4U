using Reservations.Api.Requests.Users;
using Reservations.IntegrationTests.Base;
using Reservations.IntegrationTests.Constants;
using Reservations.IntegrationTests.Helpers;
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
        var createUserRequest = new CreateUserRequest("testuser@gmail.com", InstanceConstants.ExternalUser1Id);
        var (statusCode, result) = await CreateUser<Guid>(createUserRequest, false);

        statusCode.ShouldBe(HttpStatusCode.OK);

        result.ShouldNotBe(Guid.Empty);
    }
}
