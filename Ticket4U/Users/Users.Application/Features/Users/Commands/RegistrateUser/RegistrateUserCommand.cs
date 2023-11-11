using MediatR;
using Users.Application.Models.Identity;

namespace Users.Application.Features.Users.Commands.RegistrateUser;

public class RegistrateUserCommand : IRequest<RegistrationResponse>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; } = false;
}
