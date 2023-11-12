using Microsoft.EntityFrameworkCore;
using Shared.Application.Contracts.Persistence;
using Shared.Domain;

namespace Reservations.Infrastructure.Persistance.Repositories;

public class QueryRepository<T> : IQueryRepository<T> where T : AggregateRoot
{
    protected readonly ReservationsDbContext _dbContext;

    public QueryRepository(ReservationsDbContext dbContext)
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
