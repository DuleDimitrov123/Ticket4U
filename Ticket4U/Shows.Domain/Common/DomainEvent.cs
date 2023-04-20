namespace Shows.Domain.Common;

public abstract class DomainEvent
{
    public DateTime CreationDate { get; private set; }

    public DomainEvent()
    {
        CreationDate = DateTime.Now;
    }

    public DomainEvent(DateTime creationDate)
    {
        CreationDate = creationDate;
    }
}
