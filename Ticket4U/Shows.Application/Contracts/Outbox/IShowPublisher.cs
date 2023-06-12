using Shared.Domain.Events;

namespace Shows.Application.Contracts.Outbox;

public interface IShowPublisher
{
    Task PublishCreatedShow(CreatedShowEvent createdShowEvent);
}
