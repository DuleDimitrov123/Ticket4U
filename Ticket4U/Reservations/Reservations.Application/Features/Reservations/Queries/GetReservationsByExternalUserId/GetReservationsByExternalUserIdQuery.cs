using MediatR;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservationsByExternalUserId;

public class GetReservationsByExternalUserIdQuery : IRequest<IList<ReservationResponse>>
{
    public Guid ExternalUserId { get; set; }
}
