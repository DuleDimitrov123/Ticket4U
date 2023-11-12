using Reservations.Domain.Reservations;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Contracts.Persistance;

public interface IReservationQueryRepository : IQueryRepository<Reservation>
{
    Task<IList<Reservation>> GetReservationsByUserId(Guid userId);

    Task<IList<Reservation>> GetReservationsForTheShow(Guid showId);
}
