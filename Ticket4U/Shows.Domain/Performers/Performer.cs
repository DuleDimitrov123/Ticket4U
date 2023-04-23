using Common;
using Common.Constants;
using Shows.Domain.Common;

namespace Shows.Domain.Performers;

/// <summary>
/// Performer for the show
/// </summary>
public class Performer : AggregateRoot
{
    /// <summary>
    /// Name of performer
    /// </summary>
    public string Name { get; private set; }

    //TODO: How would you add some new performer info...without deleting old...get by include would work I think and just append new...
    /// <summary>
    /// Additional performer's info
    /// </summary>
    public IList<PerformerInfo> PerformerInfos { get; private set; }

    private Performer(string name)
    {
        PerformerInfos = new List<PerformerInfo>();

        Name = name;
    }

    private Performer(string name, IList<PerformerInfo> performerInfos)
        : this(name)
    {
        PerformerInfos = performerInfos;
    }

    public static Performer Create(string name, IList<PerformerInfo>? performerInfos = null)
    {
        var errorMessages = new List<string>();

        ValidatePerformerCreation(name, errorMessages);

        if (errorMessages.Count > 0)
        {
            throw new DomainException(errorMessages);
        }

        if (performerInfos != null && performerInfos.Count > 0)
        {
            return new Performer(name, performerInfos);
        }

        return new Performer(name);
    }

    private static void ValidatePerformerCreation(string name, IList<string> errorMessages)
    {
        if (string.IsNullOrEmpty(name))
        {
            errorMessages.Add(DefaultErrorMessages.PerformerNameIsRequired);
        }

        if (name.Length > PerformerConstants.PerfomerNameMaxLenght)
        {
            errorMessages.Add(DefaultErrorMessages.PerformerNameLength);
        }
    }
}
