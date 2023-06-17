using Reservations.Domain.Shows;

namespace Reservations.Application.Contracts.Persistance;

public interface IShowRepository : IRepository<Show>
{
    Task<Show> GetShowByExternalId(Guid externalId);
}
