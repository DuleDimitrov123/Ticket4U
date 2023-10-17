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

    public async Task<AuthenticateResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.AuthenticateAsync(new AuthenticateRequest()
        {
            Email = request.Email,
            Password = request.Password
        });
    }
}
