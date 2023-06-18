using MediatR;

namespace Reservations.Application.Features.Reservations.Commands.UpdateNumberOfReservations;

public class UpdateNumberOfReservationsQuery : IRequest<Unit>
{
    public Guid Id { get; set; }

    public int NewNumberOfReservations { get; set; }
}
