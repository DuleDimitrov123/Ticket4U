using Microsoft.EntityFrameworkCore;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Reservations;

namespace Reservations.Infrastructure.Persistance.Repositories;

public class ReservationQueryRepository : QueryRepository<Reservation>, IReservationQueryRepository
{
    public ReservationQueryRepository(ReservationsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IList<Reservation>> GetReservationsByUserId(Guid userId)
    {
        return await _dbContext.Reservations
            .Where(reservation => reservation.UserId == userId)
            .ToListAsync();
    }

    public async Task<IList<Reservation>> GetReservationsForTheShow(Guid showId)
    {
        return await _dbContext.Reservations
            .Where(reservation => reservation.ShowId == showId)
            .ToListAsync();
    }
}
