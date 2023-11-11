using Microsoft.EntityFrameworkCore;
using Reservations.Application.Contracts.Persistance;
using Shared.Domain;

namespace Reservations.Infrastructure.Persistance.Repositories;

public class Repository<T> : IRepository<T> where T : AggregateRoot
{
    protected readonly ReservationsDbContext _dbContext;

    public Repository(ReservationsDbContext dbContext)
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
