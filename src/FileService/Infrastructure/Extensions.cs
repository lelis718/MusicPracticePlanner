using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MusicPracticePlanner.Configuration;
using MusicPracticePlanner.FileService.Application;
using MusicPracticePlanner.FileService.Application.Domain;
using MusicPracticePlanner.FileService.Infrastructure.Storage;

namespace MusicPracticePlanner.FileService.Infrastructure;
public static class Extensions
{

    public static void AddFileServiceInfrastructure(this WebApplicationBuilder builder)
    {
        var assembly = typeof(FileServiceApplication).Assembly;

        var commonConfig = builder.GetCommonConfiguration();
 
        builder.AddInfrastructure(assembly);

        builder.Services.AddScoped<IFileStorage>(ctx =>
        {
            
            return new FileStorage(commonConfig.Vault.Storage);
        });
    }

}
