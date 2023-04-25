using Shows.Domain.Performers;

namespace Shows.Application.Contracts.Persistance;

public interface IPerformerRepository : IRepository<Performer>
{
    Task<Performer> GetPerformerWithPerformerInfos(Guid performerId);
}
