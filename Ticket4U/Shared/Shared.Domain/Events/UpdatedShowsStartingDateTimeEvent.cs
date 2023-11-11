namespace Shared.Domain.Events;

public class UpdatedShowsStartingDateTimeEvent : DomainEvent
{
    public Guid ShowId { get; set; }

    public DateTime OldStartingDateTime { get; set; }

    public DateTime NewStartingDateTime { get; set; }

    public UpdatedShowsStartingDateTimeEvent(Guid showId, DateTime oldStartingDateTime, DateTime newStartingDateTime)
    {
        ShowId = showId;
        OldStartingDateTime = oldStartingDateTime;
        NewStartingDateTime = newStartingDateTime;
    }
}
