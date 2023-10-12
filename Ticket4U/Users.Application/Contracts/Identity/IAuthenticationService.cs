using Users.Application.Models.Identity;

namespace Users.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest request);

    Task<RegistrationResponse> RegistrateAsync(RegistrationRequest request, bool isAdmin = false);
}
