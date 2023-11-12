using Microsoft.EntityFrameworkCore;
using Shared.Application.Contracts.Persistence;
using Shared.Domain;

namespace Shows.Infrastructure.Persistance.Repositories;

public class QueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
{
    protected readonly ShowsDbContext _dbContext;

    public QueryRepository(ShowsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetById(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IList<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
}
