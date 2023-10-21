using FluentAssertions;
using GenFu;
using System.Net;
using Users.Application.Features.Users.Commands.AuthenticateUser;
using Users.Application.Models.Identity;
using Users.IntegrationTests.Base;
using Users.IntegrationTests.Constants;
using Users.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace Users.IntegrationTests.Controllers;

public class UsersControllerTests : UsersControllerHelper
{
    public UsersControllerTests(CustomWebApplicationFactory<Program> factory, ITestOutputHelper output) : base(factory, output)
    {
    }

    [Fact]
    public async Task AuthenticateSuccessfully()
    {
        var command = new AuthenticateUserCommand()
        {
            Email = TestUsers.TestAdmin.Email,
            Password = TestUsers.TestAdmin.Password
        };

        var (statusCode, result) = await Authenticate<AuthenticateResponse>(command, false);

        statusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();

        result!.Email.Should().Be(command.Email);
        result!.Token.Should().NotBeNull();
    }

    [Fact]
    public async Task AuthenticateUserNotFound()
    {
        var command = A.New<AuthenticateUserCommand>();

        var (statusCode, _) = await Authenticate<AuthenticateResponse>(command, false);

        statusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
