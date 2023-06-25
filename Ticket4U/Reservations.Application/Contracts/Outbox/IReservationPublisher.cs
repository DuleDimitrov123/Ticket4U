using Shared.Domain.Events;

namespace Reservations.Application.Contracts.Outbox;

public interface IReservationPublisher
{
    Task PublishChangedShowStatus(ChangedShowStatusEvent changedShowStatusEvent);
}
