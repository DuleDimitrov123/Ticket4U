using Microsoft.EntityFrameworkCore;
using Shows.Application.Contracts.Persistance;
using Shows.Domain.Performers;

namespace Shows.Infrastructure.Persistance.Repositories;

public class PerformerRepository : Repository<Performer>, IPerformerRepository
{
    public PerformerRepository(ShowsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Performer> GetPerformerWithPerformerInfos(Guid performerId)
    {
        return await _dbContext.Performers
            .Include(x => x.PerformerInfos)
            .Where(performer => performer.Id == performerId)
            .FirstOrDefaultAsync();
    }
}
