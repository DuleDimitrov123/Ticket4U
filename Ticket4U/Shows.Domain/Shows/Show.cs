using Shows.Domain.Common;

namespace Shows.Domain.Shows;

public class Show : AggregateRoot
{
    public ShowStatus Status { get; private set; }

    public NumberOfTickets NumberOfTickets { get; set; }
}
