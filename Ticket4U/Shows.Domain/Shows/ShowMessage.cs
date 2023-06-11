using Common;
using Common.Constants;
using Shared.Domain;

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
    public Guid ShowId { get; private set; }

    #endregion

    #region constructors

    private ShowMessage(string name, string value, Guid showId)
    {
        Name = name;
        Value = value;
        ShowId = showId;
    }

    #endregion

    public static ShowMessage Create(string name, string value, Guid showId)
    {
        IList<string> errorMessages = new List<string>();

        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.ShowMessageNameRequired);
        }

        if(name.Length > ShowConstants.ShowMessageNameMaxLength)
        {
            errorMessages.Add(DefaultErrorMessages.ShowMessageNameLength);
        }

        if (string.IsNullOrEmpty(value))
        {
            errorMessages.Add(DefaultErrorMessages.ShowMessageValueRequired);
        }

        if (value.Length > ShowConstants.ShowMessageNameMaxLength)
        {
            errorMessages.Add(DefaultErrorMessages.ShowMessageValueLength);
        }

        if(showId.Equals(Guid.Empty))
        {
            errorMessages.Add(DefaultErrorMessages.ShowMessageShowIdRequired);
        }

        if (errorMessages.Count > 0 )
        {
            throw new DomainException(errorMessages);
        }

        return new ShowMessage(name, value, showId);
    }
}
