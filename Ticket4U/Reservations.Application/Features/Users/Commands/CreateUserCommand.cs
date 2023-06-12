using MediatR;

namespace Reservations.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<Guid>
{
    public string Email { get; set; }

    public Guid ExternalId { get; set; }
}
