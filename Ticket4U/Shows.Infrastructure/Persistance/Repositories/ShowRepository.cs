using Microsoft.EntityFrameworkCore;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Shows;

namespace Shows.Infrastructure.Persistance.Repositories;

public class ShowRepository : Repository<Show>, IShowRepository
{
    public ShowRepository(ShowsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Show> GetShowWithShowMessages(Guid showId)
    {
        return await _dbContext.Shows
            .Include(show => show.ShowMessages)
            .Where(show => show.Id == showId)
            .FirstOrDefaultAsync();
    }
}
