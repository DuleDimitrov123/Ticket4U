namespace Shared.Domain.Events;

public class CreatedShowEvent : DomainEvent
{
    public string Name { get; set; }

    public DateTime StartingDateTime { get; set; }

    public int NumberOfPlaces { get; set; }

    public Guid ExternalId { get; set; }
}
