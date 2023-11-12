using Shared.Application.Contracts.Persistence;
using Shows.Domain.Shows;

namespace Shows.Application.Contracts.Persistance;

public interface IShowQueryRepository : IQueryRepository<Show>
{
    Task<Show> GetShowWithShowMessages(Guid showId);
}
