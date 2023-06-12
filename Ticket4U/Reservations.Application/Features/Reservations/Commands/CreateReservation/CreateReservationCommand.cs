using MediatR;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }

    public Guid ShowId { get; set; }

    public int NumberOfReservations { get; set; }
}
