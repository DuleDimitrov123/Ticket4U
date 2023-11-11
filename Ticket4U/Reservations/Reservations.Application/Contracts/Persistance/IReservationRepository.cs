using Reservations.Domain.Reservations;

namespace Reservations.Application.Contracts.Persistance;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<IList<Reservation>> GetReservationsByUserId(Guid userId);

    Task<IList<Reservation>> GetReservationsForTheShow(Guid showId);
}
