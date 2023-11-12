using Shared.Application.Contracts.Persistence;
using Shows.Domain.Performers;

namespace Shows.Application.Contracts.Persistance;

public interface IPerformerQueryRepository : IQueryRepository<Performer>
{
    Task<Performer> GetPerformerWithPerformerInfos(Guid performerId);
}
