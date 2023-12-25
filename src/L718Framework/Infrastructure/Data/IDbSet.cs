using System.ComponentModel;
using System.Linq.Expressions;
using L718Framework.Core.Domain.Model;

namespace L718Framework.Infrastructure.Data;

/// <summary>
/// Set representation for the Entity, an easy to use EF compatible way to add the DbContext
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityId"></typeparam>
public interface IDbSet<TEntity, TEntityId> : IQueryable<TEntity>, IListSource where TEntity : IEntity<TEntityId> {

    /// <summary>
    ///  Find entity based on the Id (Async)
    /// </summary>
    /// <param name="id">The id of the entity</param>
    /// <returns>Task result with The entity found</returns>
    Task<TEntity> FindAsync(TEntityId? id);
    
    /// <summary>
    /// Add entity Async 
    /// </summary>
    /// <param name="model">The entity to add to the dataset</param>
    /// <returns>Task Result Ok</returns>
    Task AddAsync(TEntity model);

    /// <summary>
    /// A sncronous update 
    /// Decision to make this sync is to avoid concurrency during the update process
    /// </summary>
    /// <param name="model">The model</param>
    void Update(TEntity model);

    /// <summary>
    /// A Link based search query
    /// </summary>
    /// <param name="predicate">The function to search based on the TEntity</param>
    /// <returns>A queryable list of found results</returns>
    IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

}