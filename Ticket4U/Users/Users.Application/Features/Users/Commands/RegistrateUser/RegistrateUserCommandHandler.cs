using MediatR;
using Shared.Domain.Events;
using Users.Application.Contracts.Identity;
using Users.Application.Features.Users.Notifications.UserCreated;
using Users.Application.Models.Identity;

namespace Users.Application.Features.Users.Commands.RegistrateUser;

public class RegistrateUserCommandHandler : IRequestHandler<RegistrateUserCommand, RegistrationResponse>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IMediator _mediator;

    public RegistrateUserCommandHandler(IAuthenticationService authenticationService, IMediator mediator)
    {
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    public async Task<RegistrationResponse> Handle(RegistrateUserCommand command, CancellationToken cancellationToken)
    {
        var registrationResponse = await _authenticationService.RegistrateAsync(command);

        //publish event
        await _mediator.Publish(new UserCreatedNotification(
                new CreatedUserEvent(command.Email, command.UserName, new Guid(registrationResponse.UserId))));

        return registrationResponse;
    }
}
