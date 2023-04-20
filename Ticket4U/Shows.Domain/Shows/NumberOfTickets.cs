using Shows.Domain.Common;

namespace Shows.Domain.Shows;

public class NumberOfTickets : ValueObject
{
    public int Value { get; private set; }

    private NumberOfTickets(int value)
    {
        Value = value;
    }

    public static NumberOfTickets? Create(int numberOfTickets)
    {
        //first check if numberOfTickets is > 0
        if (numberOfTickets <= 0)
        {
            return null;
        }

        return new NumberOfTickets(numberOfTickets);
    }
}