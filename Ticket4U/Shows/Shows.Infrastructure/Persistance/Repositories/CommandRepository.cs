using Microsoft.EntityFrameworkCore;
using Shared.Application.Contracts.Persistence;
using Shared.Domain;

namespace Shows.Infrastructure.Persistance.Repositories;

public class CommandRepository<T> : ICommandRepository<T> where T : AggregateRoot
{
    protected readonly ShowsDbContext _dbContext;

    public CommandRepository(ShowsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
