using Common;
using Shows.Domain.Common;

namespace Shows.Domain.Performers;

/// <summary>
/// Performer info
/// </summary>
public class PerformerInfo : Entity
{
    /// <summary>
    /// Name of performer info
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Value of performer info
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Performer id
    /// </summary>
    public Guid PerformerId { get; private set; }

    private PerformerInfo(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public static PerformerInfo Create(string name, string value)
    {
        var errorMessages = new List<string>();

        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.PerformerInfoNameRequired);
        }

        ValidatePerformerInfoValue(value, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        return new PerformerInfo(name, value);
    }

    public void UpdatePerformerInfoValue(string newValue)
    {
        var errorMessages = new List<string>();

        ValidatePerformerInfoValue(newValue, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        Value = newValue;
    }

    private static void ValidatePerformerInfoValue(string value, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(value))
        {
            errorMessages.Add(DefaultErrorMessages.PerformerInfoValueRequired);
        }
    }
}
