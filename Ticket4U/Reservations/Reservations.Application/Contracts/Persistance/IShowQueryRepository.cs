using Reservations.Domain.Shows;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Contracts.Persistance;

public interface IShowQueryRepository : IQueryRepository<Show>
{
    Task<Show> GetShowByExternalId(Guid externalId);
}
