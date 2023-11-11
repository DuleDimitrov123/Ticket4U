using MediatR;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByUserId;

public class GetReservationsByUserIdQuery : IRequest<IList<ReservationResponse>>
{
    public Guid UserId { get; set; }
}
