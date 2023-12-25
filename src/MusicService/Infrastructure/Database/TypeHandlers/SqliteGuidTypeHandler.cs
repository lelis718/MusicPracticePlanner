using System.Data;
using Dapper;

namespace MusicPracticePlanner.MusicService.Infrastructure.Database.TypeHandlers;

public class SqliteGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override Guid Parse(object value)
    {
        if(value != null) {
            return Guid.Parse(""+value);
        }
        return Guid.Empty;
        
    }

    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value.ToString().ToUpper();
    }
}