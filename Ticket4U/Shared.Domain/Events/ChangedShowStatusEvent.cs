namespace Shared.Domain.Events;

public class ChangedShowStatusEvent : DomainEvent
{
    public Guid ShowId { get; set; }

    public bool IsSoldOut { get; set; }
}
