using MediatR;
using Reservations.Application.Contracts.Outbox;

namespace Reservations.Application.Features.Shows.Notifications.ChangedShowStatus;

public class ChangedShowStatusNotificationHandler : INotificationHandler<ChangedShowStatusNotification>
{
    private readonly IReservationPublisher _reservationPublisher;

    public ChangedShowStatusNotificationHandler(IReservationPublisher reservationPublisher)
    {
        _reservationPublisher = reservationPublisher;
    }

    public async Task Handle(ChangedShowStatusNotification notification, CancellationToken cancellationToken)
    {
        await _reservationPublisher.PublishChangedShowStatus(notification.changedShowStatusEvent);
    }
}
