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
}
