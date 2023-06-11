namespace Shared.Domain;

/// <summary>
/// Base class for all domain events
/// </summary>
public abstract class DomainEvent
{
    /// <summary>
    /// Creation date of the domain event
    /// </summary>
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
