using Users.Application.Features.Users.Commands.AuthenticateUser;
using Users.Application.Features.Users.Commands.RegistrateUser;
using Users.Application.Models.Identity;

namespace Users.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateUserCommand command);

    Task<RegistrationResponse> RegistrateAsync(RegistrateUserCommand command);
}
