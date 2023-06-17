using Common;
using Common.Constants;
using Shared.Domain;
using Shared.Domain.Events;

namespace Shows.Domain.Shows;

/// <summary>
/// Cultural or artistic show
/// </summary>
public class Show : AggregateRoot
{
    #region Properties

    /// <summary>
    /// Show name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Show location
    /// </summary>
    public string Location { get; private set; }

    /// <summary>
    /// Show starting date and time
    /// </summary>
    public DateTime StartingDateTime { get; set; }

    /// <summary>
    /// Show status
    /// </summary>
    public ShowStatus Status { get; private set; }

    /// <summary>
    /// Number of places for the show
    /// </summary>
    public NumberOfPlaces NumberOfPlaces { get; private set; }

    /// <summary>
    /// Price for the ticket
    /// </summary>
    public Money TicketPrice { get; private set; }

    /// <summary>
    /// Show messages
    /// </summary>
    public IList<ShowMessage> ShowMessages { get; private set; }

    /// <summary>
    /// Show performer id
    /// </summary>
    public Guid PerformerId { get; private set; }

    /// <summary>
    /// Show category id
    /// </summary>
    public Guid CategoryId { get; private set; }

    #endregion

    #region constructors

    //Required for EF Core in order to create migration
    //When getting from db, this ctor is called
    private Show()
    {
        ShowMessages = new List<ShowMessage>();
    }

    private Show(string name, string location, NumberOfPlaces numberOfPlaces,
        Money ticketPrice, DateTime startingDateTime, Guid performerId, Guid categoryId)
    {
        ShowMessages = new List<ShowMessage>();

        Name = name;
        Location = location;
        NumberOfPlaces = numberOfPlaces;
        TicketPrice = ticketPrice;
        StartingDateTime = startingDateTime;
        PerformerId = performerId;
        CategoryId = categoryId;

        Status = ShowStatus.HasTickets;
    }

    #endregion

    #region domain logic

    public static Show Create(string name, string location, NumberOfPlaces numberOfPlaces,
        Money ticketPrice, DateTime startingDateTime, Guid performerId, Guid categoryId)
    {
        IList<string> errorMessages = new List<string>();

        ValidateShowCreate(name, location, numberOfPlaces, ticketPrice, startingDateTime, performerId, categoryId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Show(name, location, numberOfPlaces, ticketPrice, startingDateTime, performerId, categoryId);
    }

    public void UpdateStartingDateTime(DateTime newStartingDateTime)
    {
        ShowMessages.Add(
            ShowMessage.Create(
                ShowConstants.ShowIsPostponedName,
                ShowConstants.ShowIsPostponedValue.Replace("{oldDateTime}", StartingDateTime.ToString())
                    .Replace("{newDateTime}", newStartingDateTime.ToString()),
                Id));

        StartingDateTime = newStartingDateTime;
    }

    public void UpdateShowName(string newName)
    {
        IList<string> errorMessages = new List<string>();

        ValidateShowName(newName, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        Name = newName;
    }

    public void UpdateShowLocation(string newLocation)
    {
        IList<string> errorMessages = new List<string>();

        ValidateShowLocation(newLocation, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        Location = newLocation;
    }

    public void UpdateTicketPriceAmount(decimal newAmount)
    {
        //TicketPrice is Money, which is Value Object, don't update it, create new!
        TicketPrice = Money.Create(TicketPrice.Currency, newAmount);
    }

    public static void ValidateShowCreate(string name, string location, NumberOfPlaces numberOfPlaces,
        Money ticketPrice, DateTime startingDateTime, Guid performerId, Guid categoryId, IList<string> errorMessages)
    {
        ValidateShowName(name, errorMessages);

        ValidateShowLocation(location, errorMessages);

        if (startingDateTime < DateTime.Now)
        {
            errorMessages.Add(DefaultErrorMessages.ShowStartingDateTimeNotInFuture);
        }

        if (performerId.Equals(Guid.Empty))
        {
            errorMessages.Add(DefaultErrorMessages.ShowPerformerIdRequired);
        }

        if (categoryId.Equals(Guid.Empty))
        {
            errorMessages.Add(DefaultErrorMessages.ShowCategoryIdRequired);
        }
    }

    public void AddShowMessage(string showMessageName, string showMessageValue)
    {
        ShowMessages.Add(ShowMessage.Create(showMessageName, showMessageValue, Id));
    }

    private static void ValidateShowName(string name, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameIsRequired);
        }

        if (name.Length > ShowConstants.ShowNameMaxLenght)
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameLength);
        }
    }

    private static void ValidateShowLocation(string location, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(location))
        {
            errorMessages.Add(DefaultErrorMessages.ShowLocationIsRequired);
        }
    }

    #endregion
}
