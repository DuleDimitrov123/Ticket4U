using MediatR;

namespace Reservations.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public string Email { get; set; }

    public string UserName { get; set; }
}
