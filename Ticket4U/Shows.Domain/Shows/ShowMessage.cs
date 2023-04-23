using Shows.Domain.Common;

namespace Shows.Domain.Shows;

/// <summary>
/// Some additional information/messages about the show, eg. it is rescheduled
/// </summary>
public class ShowMessage : Entity
{
    #region properties

    /// <summary>
    /// Show message name
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Show message value
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Show id
    /// </summary>
    public Guid ShowId { get; set; }

    #endregion

    #region constructors

    private ShowMessage(string name, string value, Guid showId)
    {
        Name = name;
        Value = value;
        ShowId = showId;
    }

    #endregion
}
