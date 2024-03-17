using Reservations.Domain.Users;
using Shared.Application.Contracts.Persistence;

namespace Reservations.Application.Contracts.Persistance;

public interface IUserQueryRepository : IQueryRepository<User>
{
    Task<User?> GetUserByExternalId(Guid userExternalId);
}
