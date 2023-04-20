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

    #endregion

    #region constructors

    private Show(string name, string location, ShowStatus showStatus, NumberOfPlaces numberOfPlaces, Money ticketPrice, Guid performerId)
    {
        ShowMessages = new List<ShowMessage>();

        Name = name;
        Location = location;
        Status = showStatus;
        NumberOfPlaces = numberOfPlaces;
        TicketPrice = ticketPrice;
        PerformerId = performerId;
    }

    #endregion

    #region domain logic

    //public static Show Create(string name, string location, ShowStatus showStatus, NumberOfPlaces numberOfPlaces, Money ticketPrice, Guid performerId)
    //{
    //    if (string.IsNullOrEmpty(name))
    //    {
    //        //throw DomainException
    //        throw new ArgumentNullException("name");
    //    }
    //}

    #endregion
}
