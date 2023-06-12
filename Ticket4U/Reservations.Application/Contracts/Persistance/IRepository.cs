using Shared.Domain;

namespace Reservations.Application.Contracts.Persistance;

public interface IRepository<T> where T : AggregateRoot
{
    Task<T> GetById(Guid id);

    Task<IList<T>> GetAll();

    Task<T> Add(T entity);

    Task Delete(T entity);

    Task Update(T entity);
}
