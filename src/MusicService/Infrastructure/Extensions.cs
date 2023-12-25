
using System.Data;
using System.Runtime.CompilerServices;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPracticePlanner.Configuration;
using MusicPracticePlanner.MusicService.Application;
using MusicPracticePlanner.MusicService.Application.Domain;
using MusicPracticePlanner.MusicService.Infrastructure.Database;
using MusicPracticePlanner.MusicService.Infrastructure.Database.Migrations;
using MusicPracticePlanner.MusicService.Infrastructure.Database.TypeHandlers;

namespace MusicService.Infrastructure;
public static class Extensions
{

    public static void AddMusicServiceInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(MusicApplication).Assembly;
        builder.AddInfrastructure(assembly);
        
        var commonConfig = builder.GetCommonConfiguration();
        string connectionString = GetDatabaseConnectionString(commonConfig.Vault.Database);
        builder.ConfigureDatabase();
        builder.ConfigureMigrations(connectionString);
        builder.Services.AddTransient<IMusicRpository, MusicRepository>(ctx =>
        {
            return new MusicRepository(new SqliteConnection(connectionString));
        });
    }

    public static void StartupMusicMigration(this WebApplication app){
        var serviceScope = app.Services.CreateScope();
        var runner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static void ConfigureDatabase(this WebApplicationBuilder builder){
        Dapper.SqlMapper.AddTypeHandler(new SqliteGuidTypeHandler());
        Dapper.SqlMapper.RemoveTypeMap(typeof(Guid));
        Dapper.SqlMapper.RemoveTypeMap(typeof(Guid?));
    }
    
    private static void ConfigureMigrations(this WebApplicationBuilder builder, string connectionString)
    {
        builder.Services.AddFluentMigratorCore();
        builder.Services.ConfigureRunner(cfg =>
        {
            cfg.AddSQLite();
            cfg.WithGlobalConnectionString(connectionString);
            cfg.ScanIn(typeof(AddMusicTable).Assembly);
        });
        builder.Services.AddLogging(log=>log.AddFluentMigratorConsole());
        builder.Services.BuildServiceProvider(false);
    }
    

    private static string GetDatabaseConnectionString(string databaseFile){
        if(!File.Exists(databaseFile)){
            Directory.CreateDirectory(Path.GetDirectoryName(databaseFile));
            File.Create(databaseFile);
        }
        System.Console.WriteLine("Criando arquivo" + databaseFile);
        return $"Data Source={databaseFile};";
    }

}
