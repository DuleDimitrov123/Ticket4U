using Shared.Domain.Events;

namespace Users.Application.Contracts.Outbox;

public interface IUserPublisher
{
    Task PublishCreatedUser(CreatedUserEvent createdUserEvent);
}
