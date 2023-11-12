using Shared.Domain;

namespace Shared.Application.Contracts.Persistence;

public interface IQueryRepository<T> where T : AggregateRoot
{
    Task<T> GetById(Guid id);

    Task<IList<T>> GetAll();
}
