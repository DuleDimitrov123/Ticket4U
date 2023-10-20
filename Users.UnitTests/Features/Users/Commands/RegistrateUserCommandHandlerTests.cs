﻿using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Users.Application.Contracts.Identity;
using Users.Application.Features.Users.Commands.RegistrateUser;
using Users.Application.Models.Identity;
using Users.UnitTests.Helpers;

namespace Users.UnitTests.Features.Users.Commands;

public class RegistrateUserCommandHandlerTests
{
    [Theory]
    [AutoMoqData]
    public async Task RegistrateUser(
        [Frozen] Mock<IAuthenticationService> authenticationServiceMock,
        //Mock<IMediator> mediatorMock,
        RegistrateUserCommandHandler handler)
    {
        var registrationResponse = new RegistrationResponse() { UserId = Guid.NewGuid().ToString() };
        authenticationServiceMock.Setup(x => x.RegistrateAsync(It.IsAny<RegistrationRequest>(), It.IsAny<bool>()))
            .ReturnsAsync(registrationResponse);

        //mediatorMock.Setup(x => x.Publish(It.IsAny<UserCreatedNotification>(), default)).Returns(Task.CompletedTask);

        var command = new RegistrateUserCommand()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "email@gmail.com",
            UserName = "UserName",
            Password = "ProbaPass&76"
        };

        var result = await handler.Handle(command, default);

        result.Should().NotBeNull();

        result.Should().BeEquivalentTo(registrationResponse);

        //mediatorMock.Verify(
        //    x => x.Publish(It.IsAny<UserCreatedNotification>(), default),
        //    Times.Once);
    }
}
