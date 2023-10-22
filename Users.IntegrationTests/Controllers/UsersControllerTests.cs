using FluentAssertions;
using Shared.Api.Middlewares;
using System.Net;
using Users.Api.Controllers.Requests;
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
    public async Task AuthenticateUser_Successfully()
    {
        var request = new AuthenticateUserRequest(TestUsers.TestAdmin.Email, TestUsers.TestAdmin.Password);

        var (statusCode, result) = await Authenticate<AuthenticateResponse>(request, false);

        statusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();

        result!.Email.Should().Be(request.Email);
        result!.Token.Should().NotBeNull();
    }

    [Fact]
    public async Task AuthenticateUser_NotFound()
    {
        var request = new AuthenticateUserRequest("testemail@gmail.com", "password123/9");

        var (statusCode, result) = await Authenticate<ErrorResponse>(request, false);

        statusCode.Should().Be(HttpStatusCode.NotFound);

        result.Should().NotBeNull();
        result!.ExceptionMessages.Should().Contain($"User {request.Email} is not found");
    }

    [Fact]
    public async Task AuthenticateUser_WrongPassword()
    {
        var request = new AuthenticateUserRequest(TestUsers.TestAdmin.Email, "WRONGPASSWORD");

        var (statusCode, result) = await Authenticate<ErrorResponse>(request, false);

        statusCode.Should().Be(HttpStatusCode.BadRequest);

        result.Should().NotBeNull();
        result!.ExceptionMessages.Should().Contain($"Credentials for '{request.Email} aren't valid!'");
    }

    [Fact]
    public async Task RegisterUser_Successfully()
    {
        var request = new RegisterUserRequest("FirstName", "LastName", "something@gmail.com", "Some", "TestPass12*4NotReal");

        var (statusCode, result) = await Register<RegistrationResponse>(request, false);

        statusCode.Should().Be(HttpStatusCode.OK);

        result.Should().NotBeNull();
    }
}
