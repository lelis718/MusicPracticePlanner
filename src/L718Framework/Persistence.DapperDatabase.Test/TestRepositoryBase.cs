using Dapper;
using L718Framework.Infrastructure.Logging;
using L718Framework.Persistence.DapperDatabase;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace Persistence.DapperDatabase.Test;

public class TestRepositoryBase : IDisposable
{
    private RepositoryBase<EntitySample, int> _repository;
    private SqliteConnection databaseSample;

    public TestRepositoryBase()
    {
        LogService.InitializeLogService(LoggerFactory.Create(cfg=>{
            cfg.SetMinimumLevel(LogLevel.Information);
            cfg.AddConsole();
        }));
        CreateDatabase();
        CreateEntityTable();
        _repository = new RepositoryMock(databaseSample);
    }
    public void Dispose()
    {
        databaseSample.Close();
    }

    private void CreateDatabase()
    {
        string connectionString = $"Data Source=:memory:;";
        databaseSample= new SqliteConnection(connectionString);
        databaseSample.Open();
    }
    private void CreateEntityTable(){
        string createTableCommand = "CREATE TABLE IF NOT EXISTS EntitySamples (Id int PRIMARY KEY, Name TEXT, CreationDate DATETIME);";
        databaseSample.Execute(createTableCommand);
    }


    [Fact]
    public async Task TestThatRepositoryCanAddEntities()
    {
        var someElement = new EntitySample(1, "test");
        
        
        await _repository.AddAsync(someElement);


        var result = databaseSample.Query<EntitySample>("SELECT * FROM EntitySamples");
        Assert.Contains(result,item=>item.Id== someElement.Id );
    }


    [Fact]
    public async Task TestThatRepositoryCanUpdateEntities()
    {
        var someElement = new EntitySample(1, "test");
        await _repository.AddAsync(someElement);

        someElement.Name="UPDATED";
        await _repository.UpdateAsync(someElement);

        var result = databaseSample.Query<EntitySample>("SELECT * FROM EntitySamples");
        Assert.Contains(result,item=>item.Id== someElement.Id && item.Name == "UPDATED" );
    }    

    [Fact]
    public async Task TestThatRepositoryCanDeleteEntities()
    {
        var someElement = new EntitySample(1, "test");
        await _repository.AddAsync(someElement);

        await _repository.DeleteAsync(someElement);

        var result = databaseSample.Query<EntitySample>("SELECT * FROM EntitySamples");
        Assert.DoesNotContain(result,item=>item.Id== someElement.Id );
    }

    
    [Fact]
    public async Task TestThatRepositoryCanRetrieveASingleEntity()
    {
        var someElement = new EntitySample(1, "test");
        var someOtherElement = new EntitySample(2, "test2");
        await _repository.AddAsync(someElement);
        await _repository.AddAsync(someOtherElement);

        var result = await _repository.FindByIdAsync(2);
        Assert.NotNull(result);
        Assert.Equal(2, result.Id);
    }


}
