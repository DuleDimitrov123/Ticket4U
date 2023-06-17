using Microsoft.EntityFrameworkCore;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Shows;

namespace Reservations.Infrastructure.Persistance.Repositories;

public class ShowRepository : Repository<Show>, IShowRepository
{
    public ShowRepository(ReservationsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Show> GetShowByExternalId(Guid externalId)
    {
        return await _dbContext.Shows
            .Where(show => show.ExternalId == externalId)
            .FirstOrDefaultAsync();
    }
}
