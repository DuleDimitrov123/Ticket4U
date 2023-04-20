using System.Collections.ObjectModel;

namespace Shows.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected readonly IList<DomainEvent> DomainEvents = new List<DomainEvent>();

    protected void AddEvent(DomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    //Delete events after saving
    public void ClearEvents()
    {
        DomainEvents.Clear();
    }

    public ReadOnlyCollection<DomainEvent> GetDomainEvents() => DomainEvents?.AsReadOnly();
}
