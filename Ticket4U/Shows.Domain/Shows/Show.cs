using Common;
using Common.Constants;
using Shows.Domain.Common;
using Shows.Domain.Performers;

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
    public Guid CategoryId { get; set; }

    #endregion

    #region constructors

    private Show(string name, string location, ShowStatus showStatus, NumberOfPlaces numberOfPlaces, 
        Money ticketPrice, Guid performerId, Guid categoryId)
    {
        ShowMessages = new List<ShowMessage>();

        Name = name;
        Location = location;
        Status = showStatus;
        NumberOfPlaces = numberOfPlaces;
        TicketPrice = ticketPrice;
        PerformerId = performerId;
        CategoryId = categoryId;
    }

    #endregion

    #region domain logic

    public static Show Create(string name, string location, ShowStatus showStatus, NumberOfPlaces numberOfPlaces, 
        Money ticketPrice, Guid performerId, Guid categoryId)
    {
        IList<string> errorMessages = new List<string>();

        ValidateShowCreate(name, location, showStatus, numberOfPlaces, ticketPrice, performerId, categoryId, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new Show(name, location, showStatus, numberOfPlaces, ticketPrice, performerId, categoryId);
    }

    public static void ValidateShowCreate(string name, string location, ShowStatus showStatus, NumberOfPlaces numberOfPlaces,
        Money ticketPrice, Guid performerId, Guid categoryId, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameIsRequired);
        }

        if(name.Length > ShowConstants.ShowNameMaxLenght)
        {
            errorMessages.Add(DefaultErrorMessages.ShowNameLength);
        }

        if (string.IsNullOrEmpty(location))
        {
            errorMessages.Add(DefaultErrorMessages.ShowLocationIsRequired);
        }

        if (name.Length > ShowConstants.ShowNameMaxLenght)
        {
            errorMessages.Add(DefaultErrorMessages.ShowLocationLength);
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

    #endregion
}
