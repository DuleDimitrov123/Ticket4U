using MediatR;
using Users.Application.Contracts.Identity;
using Users.Application.Models.Identity;

namespace Users.Application.Features.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateResponse>
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticateUserCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        return await _authenticationService.AuthenticateAsync(command);
    }
}
