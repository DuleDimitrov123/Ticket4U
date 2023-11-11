using Common;
using Shared.Domain;

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
        IList<string> errorMessages = new List<string>();

        //first check if numberOfTickets is > 0
        if (numberOfTickets <= 0)
        {
            errorMessages.Add(DefaultErrorMessages.NumberOfPlacesGreaterThan0);
        }

        if(errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new NumberOfPlaces(numberOfTickets);
    }
}