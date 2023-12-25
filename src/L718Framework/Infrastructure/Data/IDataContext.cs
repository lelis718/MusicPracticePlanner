using L718Framework.Core.Domain.Model;

namespace L718Framework.Infrastructure.Data;
/// <summary>
/// A Data Context wrapper for the database integration files
/// The context is used within the repository base class
/// </summary>
public interface IDataContext {

    /// <summary>
    /// The entity set from this datacontext
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    /// <typeparam name="IdType">the Type of the entity</typeparam>
    /// <returns></returns>
    IDbSet<TEntity,IdType> EntitySet<TEntity, IdType>() where TEntity: IEntity<IdType>;

    /// <summary>
    /// Entity Set for saving asynchronously the entity
    /// </summary>
    /// <param name="cancellationToken">The cancelation Token</param>
    /// <returns></returns>
    Task SaveAsync(CancellationToken cancellationToken=default);

    /// <summary>
    /// A Method to check if the model was detached from the database context
    /// This is useful to check while saving data
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="IdType"></typeparam>
    /// <param name="model"></param>
    /// <returns></returns>
    bool IsModelDetached<TEntity, IdType>(TEntity model) where TEntity:IEntity<IdType>;

}