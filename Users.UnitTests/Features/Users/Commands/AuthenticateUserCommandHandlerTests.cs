using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Users.Application.Contracts.Identity;
using Users.Application.Features.Users.Commands.AuthenticateUser;
using Users.Application.Models.Identity;
using Users.UnitTests.Helpers;

namespace Users.UnitTests.Features.Users.Commands;

public class AuthenticateUserCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task AuthenticateUser(
        [Frozen] Mock<IAuthenticationService> authenticationServiceMock,
        AuthenticateUserCommandHandler handler)
    {
        var authenticateResponse = new AuthenticateResponse()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Username",
            Email = "email@gmail.com",
            Token = "SomeToken"
        };

        authenticationServiceMock.Setup(x => x.AuthenticateAsync(It.IsAny<AuthenticateRequest>()))
            .ReturnsAsync(authenticateResponse);

        var command = new AuthenticateUserCommand()
        {
            Email = authenticateResponse.Email,
            Password = "SomePass"
        };

        var result = await handler.Handle(command, default);

        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(authenticateResponse);
    }
}
