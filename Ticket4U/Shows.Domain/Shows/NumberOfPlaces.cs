using Shows.Domain.Common;

namespace Shows.Domain.Shows;

/// <summary>
/// Number of tickets
/// </summary>
public record NumberOfPlaces : ValueObject
{
    public int Value { get; private set; }

    private NumberOfPlaces(int value)
    {
        Value = value;
    }

    public static NumberOfPlaces Create(int numberOfTickets)
    {
        //first check if numberOfTickets is > 0
        if (numberOfTickets <= 0)
        {
            //TODO: throw domain exception
            return null;
        }

        return new NumberOfPlaces(numberOfTickets);
    }
}