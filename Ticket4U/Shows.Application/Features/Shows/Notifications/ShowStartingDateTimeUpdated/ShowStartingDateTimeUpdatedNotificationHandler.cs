using MediatR;
using Shows.Application.Contracts.Outbox;

namespace Shows.Application.Features.Shows.Notifications.ShowStartingDateTimeUpdated;

public class ShowStartingDateTimeUpdatedNotificationHandler : INotificationHandler<ShowStartingDateTimeUpdatedNotification>
{
    private readonly IShowPublisher _showPublisher;

    public ShowStartingDateTimeUpdatedNotificationHandler(IShowPublisher showPublisher)
    {
        _showPublisher = showPublisher;
    }

    public async Task Handle(ShowStartingDateTimeUpdatedNotification notification, CancellationToken cancellationToken)
    {
        await _showPublisher.PublishUpdatedShowDateTime(notification.updatedShowsStartingDateTimeEvent);
    }
}
