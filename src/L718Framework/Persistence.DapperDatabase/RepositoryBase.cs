using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;
using Dapper;
using L718Framework.Core.Domain;
using L718Framework.Core.Domain.Model;
using L718Framework.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace L718Framework.Persistence.DapperDatabase;

/// <summary>
/// Dapper Implementation of the Repository in the L718Framework.
/// Framework based on "Mastering Dapper with the Generic Repository Pattern" by 
/// Zuraiz Ahmed Shehzad - http://tinyurl.com/3x5ukhwy
///  
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TIdType"></typeparam>
public class RepositoryBase<TEntity, TIdType> : IRepository<TEntity, TIdType> where TEntity : IEntity<TIdType>
{
    private static ILogger LOG = LogService.GetLogger(typeof(RepositoryBase<TEntity, TIdType>));
    protected IDbConnection Connection{get;private set;}
    

    public RepositoryBase(IDbConnection dbConnection)
    {
        Connection = dbConnection;
    }

    public async Task<TEntity> FindByIdAsync(TIdType id)
    {
        TEntity? result = default;
        try
        {
            string tableName = GetTableName();
            string keyColumn = GetKeyColumnName();
            string query = $"SELECT * FROM {tableName} WHERE {keyColumn} = '{id}'";

            result = await Connection.QueryFirstAsync<TEntity>(query);
        }
        catch (Exception ex)
        {
            LOG.LogError($"Fail to query database; {ex.Message}");
        }
        return result;
    }


    public async Task AddAsync(TEntity model)
    {
        string tableName = GetTableName();
        string columns = GetColumns(excludeKey: true);
        string properties = GetPropertyNames(excludeKey: true);
        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

        try
        {

            await Connection.ExecuteAsync(query, model);
        }
        catch (Exception ex)
        {
            LOG.LogError($"Fail to add entity to database; {ex.Message} - {query}");
        }
    }

    public async Task UpdateAsync(TEntity model)
    {
        string tableName = GetTableName();
        string keyColumn = GetKeyColumnName();
        string keyProperty = GetKeyPropertyName();
        StringBuilder query = new StringBuilder();
        query.Append($"UPDATE {tableName} SET ");

        foreach (var property in GetProperties(true))
        {
            var columnAttr = property.GetCustomAttribute<ColumnAttribute>();

            string propertyName = property.Name;
            string columnName = columnAttr.Name;

            query.Append($"{columnName} = @{propertyName},");
        }
        query.Remove(query.Length - 1, 1);

        query.Append($" WHERE {keyColumn} = @{keyProperty}");

        try
        {
            await Connection.ExecuteAsync(query.ToString(), model);
        }
        catch (Exception ex)
        {
            LOG.LogError($"Fail to update entity to database; {ex.Message} - {query}");
        }

    }

    public async Task DeleteAsync(TEntity model)
    {
        string tableName = GetTableName();
        string keyColumn = GetKeyColumnName();
        string keyProperty = GetKeyPropertyName();
        string query = $"DELETE FROM {tableName} WHERE {keyColumn} = @{keyProperty}";

        try
        {

            await Connection.ExecuteAsync(query, model);
        }
        catch (Exception ex)
        {
            LOG.LogError($"Fail to delete entity from database; {ex.Message} - {query}");
        }

    }

    private string GetTableName()
    {
        string tableName = "";
        var type = typeof(TEntity);
        var tableAttr = type.GetCustomAttribute<TableAttribute>();
        if (tableAttr != null)
        {
            tableName = tableAttr.Name;
            return tableName;
        }

        return type.Name + "s";
    }

    public static string? GetKeyColumnName()
    {
        PropertyInfo[] properties = typeof(TEntity).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object[] keyAttributes = property.GetCustomAttributes(typeof(TIdType), true);

            if (keyAttributes != null && keyAttributes.Length > 0)
            {
                object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes != null && columnAttributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                    return columnAttribute.Name;
                }
                else
                {
                    return property.Name;
                }
            }
        }

        return null;
    }


    private string GetColumns(bool excludeKey = false)
    {
        var type = typeof(TEntity);
        var columns = string.Join(", ", type.GetProperties()
            .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
            .Select(p =>
            {
                var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                return columnAttr != null ? columnAttr.Name : p.Name;
            }));

        return columns;
    }

    protected string GetPropertyNames(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        var values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }

    protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        return properties;
    }

    protected string? GetKeyPropertyName()
    {
        var properties = typeof(TEntity).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

        if (properties.Any())
        {
            return properties.FirstOrDefault()?.Name;
        }

        return null;
    }

}