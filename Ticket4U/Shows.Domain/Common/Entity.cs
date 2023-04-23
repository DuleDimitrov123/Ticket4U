namespace Shows.Domain.Common;

/// <summary>
/// Base class for all entities
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Identifier for all entities
    /// </summary>
    public Guid Id { get; protected set; }
}
