using Shows.Domain.Shows;

namespace Shows.Application.Contracts.Persistance;

public interface IShowRepository : IRepository<Show>
{
    Task<Show> GetShowWithShowMessages(Guid showId);
}
