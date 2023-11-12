using Shared.Domain;

namespace Shared.Application.Contracts.Persistence;

public interface ICommandRepository<T> where T : AggregateRoot
{
    Task<T> Add(T entity);

    Task Delete(T entity);

    Task Update(T entity);
}
