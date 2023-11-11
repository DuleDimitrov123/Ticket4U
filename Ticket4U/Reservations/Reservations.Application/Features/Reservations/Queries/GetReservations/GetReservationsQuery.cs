using MediatR;
using Reservations.Application.Features.Reservations.Responses;

namespace Reservations.Application.Features.Reservations.Queries.GetReservations;

public class GetReservationsQuery : IRequest<IList<ReservationResponse>>
{
}
