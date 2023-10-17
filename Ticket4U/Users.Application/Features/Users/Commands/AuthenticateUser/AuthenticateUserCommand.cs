using MediatR;
using Users.Application.Models.Identity;

namespace Users.Application.Features.Users.Commands.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<AuthenticateResponse>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
