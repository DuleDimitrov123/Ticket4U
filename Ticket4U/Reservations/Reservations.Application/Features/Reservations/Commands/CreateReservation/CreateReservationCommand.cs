using MediatR;

namespace Reservations.Application.Features.Reservations.Commands.CreateReservation;

public class CreateReservationCommand : IRequest<Guid>
{
    public Guid ExternalUserId { get; set; }

    public Guid ExternalShowId { get; set; }

    public int NumberOfReservations { get; set; }
}
