namespace L718Framework.Core.Domain.Model;


/// <summary>
/// Interface for entity related classes
/// </summary>
/// <typeparam name="IdType">The type of the Id / Primary Key </typeparam>
public interface IEntity<IdType>
{
    /// <summary>
    /// The Id or Primary Key 
    /// </summary>
    public IdType Id { get; }

    /// <summary>
    /// The Creation Date of this Entity
    /// </summary>
    public DateTime CreationDate { get; }
}
