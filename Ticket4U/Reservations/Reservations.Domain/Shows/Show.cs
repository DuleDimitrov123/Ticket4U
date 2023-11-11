using Reservations.Common;
using Reservations.Common.Constants;
using Shared.Domain;

namespace Reservations.Domain.Shows;

public class Show : AggregateRoot
{
    public string Name { get; private set; }

    public DateTime StartingDateTime { get; private set; }

    public int NumberOfPlaces { get; private set; }

    //represents ShowStatus from Shows microservice
    public bool IsSoldOut { get; private set; }

    public Guid ExternalId { get; private set; }

    private Show(string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId)
    {
        Name = name;
        StartingDateTime = startingDateTime;
        NumberOfPlaces = numberOfPlaces;
        ExternalId = externalId;
        IsSoldOut = false;
    }

    private Show(Guid showId, string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId)
    {
        Id = showId;
        Name = name;
        StartingDateTime = startingDateTime;
        NumberOfPlaces = numberOfPlaces;
        ExternalId = externalId;
        IsSoldOut = false;
    }

    public static Show Create(Guid showId, string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId)
    {
        var errorMessages = new List<string>();

        ValidateShowCreation(showId, name, startingDateTime, numberOfPlaces, externalId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Show(showId, name, startingDateTime, numberOfPlaces, externalId);
    }

    public static Show Create(string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId)
    {
        var errorMessages = new List<string>();

        ValidateShowCreation(name, startingDateTime, numberOfPlaces, externalId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Show(name, startingDateTime, numberOfPlaces, externalId);
    }

    public void UpdateStartingDateTime(DateTime newStartingDateTime)
    {
        StartingDateTime = newStartingDateTime;
    }

    public void SellOutTheShow()
    {
        IsSoldOut = true;
    }

    public void UnSellOutTheShow()
    {
        IsSoldOut = false;
    }

    private static void ValidateShowCreation(string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId, List<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameIsRequired);
        }

        if (name.Length > ShowConstants.ShowNameMaxLenght)
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameLength);
        }

        if (startingDateTime < DateTime.Now)
        {
            errorMessages.Add(DefaultErrorMessages.ShowStartingDateTimeNotInFuture);
        }

        if (numberOfPlaces <= 0)
        {
            errorMessages.Add(DefaultErrorMessages.NumberOfPlacesGreaterThan0);
        }
    }

    private static void ValidateShowCreation(Guid showId, string name, DateTime startingDateTime, int numberOfPlaces, Guid externalId, List<string> errorMessages)
    {
        if (showId == Guid.Empty)
        {
            errorMessages.Add("CANNOT CREATE SHOW WITH EMPTY GUID!");
        }

        ValidateShowCreation(name, startingDateTime, numberOfPlaces, externalId, errorMessages);
    }
}
