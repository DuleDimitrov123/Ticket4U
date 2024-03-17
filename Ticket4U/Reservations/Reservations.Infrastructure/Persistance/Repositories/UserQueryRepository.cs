using Microsoft.EntityFrameworkCore;
using Reservations.Application.Contracts.Persistance;
using Reservations.Domain.Users;

namespace Reservations.Infrastructure.Persistance.Repositories;

public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
{
    public UserQueryRepository(ReservationsDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserByExternalId(Guid userExternalId)
    {
        return await _dbContext
            .Users
            .Where(user => user.ExternalId == userExternalId)
            .FirstOrDefaultAsync();
    }
}
