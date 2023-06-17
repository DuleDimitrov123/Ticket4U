using MediatR;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationById;

public class GetReservationByIdQuery : IRequest<ReservationResponse>
{
    public Guid ReservationId { get; set; }
}
