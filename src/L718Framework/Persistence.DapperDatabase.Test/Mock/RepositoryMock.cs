using System.Data;
using L718Framework.Persistence.DapperDatabase;

public class RepositoryMock : RepositoryBase<EntitySample, int>
{
    public RepositoryMock(IDbConnection dbConnection) : base(dbConnection)
    {
    }
}