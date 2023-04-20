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

    public static void Create(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            //TODO: throw domain exception
            throw new ArgumentNullException("name");
        }

        //TODO: add constant for 100
        if (name.Length >= 100)
        {
            //TODO: throw new domain exception
            throw new Exception("Long name of performer");
        }
    }
}
