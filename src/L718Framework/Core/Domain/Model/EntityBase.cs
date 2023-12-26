using System.ComponentModel.DataAnnotations;

namespace L718Framework.Core.Domain.Model;


/// <summary>
/// A generic implementation for the entity base based on Guid Ids
/// </summary>
public class EntityBase : EntityBase<Guid>
{
    public EntityBase() : base(Guid.NewGuid())
    {}
}

/// <summary>
/// A generic implementation for the enty base with some common logic for small sytems
/// </summary>
/// <typeparam name="IdType">The type of the Id / Primary Key </typeparam>
public class EntityBase<IdType>:IEntity<IdType>
{
    public EntityBase(IdType id){
        this.Id = id;
        this.CreationDate = DateTime.Now;
    }

    /// <summary>
    /// The Id or Primary Key 
    /// </summary>
    [Key]
    public IdType Id { get; protected set; }

    /// <summary>
    /// The Creation Date of this Entity
    /// </summary>
    public DateTime CreationDate { get; protected set; }
}
