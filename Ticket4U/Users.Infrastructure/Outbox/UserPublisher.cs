using DotNetCore.CAP;
using Shared.Domain.Events;
using Shared.Domain.Events.Constants;
using Users.Application.Contracts.Outbox;
using Users.Infrastructure.Identity;

namespace Users.Infrastructure.Outbox;

public class UserPublisher : IUserPublisher
{
    private readonly ICapPublisher _capPublisher;
    private readonly UsersIdentityDbContext _dbContext;

    public UserPublisher(ICapPublisher capPublisher, UsersIdentityDbContext dbContext)
    {
        _capPublisher = capPublisher;
        _dbContext = dbContext;
    }

    public async Task PublishCreatedUser(CreatedUserEvent createdUserEvent)
    {
        using (var transaction = _dbContext.Database.BeginTransaction(_capPublisher, true))
        {
            await _capPublisher.PublishAsync(Ticket4UDomainEventsConstants.NewUserCreated, createdUserEvent);
        }
    }
}
