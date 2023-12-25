using L718Framework.Core.Domain.Model;

namespace L718Framework.Core.Domain;


/// <summary>
/// Repository Interface for Write Actions
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="IdType"></typeparam>
public interface IWriteRepository<T, IdType> where T : IEntity<IdType>
{
    Task AddAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
}

/// <summary>
/// Repository interface for read only actions
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="IdType"></typeparam>
public interface IReadRepository<T, IdType> where T : IEntity<IdType>
{
    Task<T> FindByIdAsync(IdType id);

}

/// <summary>
/// Main repository for base crud applications evolving the read and write actions 
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T, IdType> : IWriteRepository<T, IdType>, IReadRepository<T, IdType> where T : IEntity<IdType>
{ }

/// <summary>
/// Repository access for a standar GUID implmentation.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> : IRepository<T, Guid> where T : IEntity<Guid> { }
