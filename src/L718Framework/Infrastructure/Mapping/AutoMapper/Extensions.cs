using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace L718Framework.Infrastructure.Mapping.AutoMapper;

/// <summary>
/// Main Configuration Extension for AutoMapper
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Installs and Configures the AutoMapper for the current assembly 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="currentAssembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapperExtension(this IServiceCollection services, Assembly currentAssembly)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(currentAssembly);
        });
        return services;
    }

    
}

