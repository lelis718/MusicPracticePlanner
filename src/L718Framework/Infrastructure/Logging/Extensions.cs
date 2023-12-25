using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace L718Framework.Infrastructure.Logging;

/// <summary>
/// Common extensions for Logging Engine
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Initializes the logging engine
    /// </summary>
    /// <param name="app"></param>
    /// <param name="applicationAssembly"></param>
    /// <returns></returns>
    public static WebApplication InitializeLogging(this WebApplication app)
    {
        ILoggerFactory service = app.Services.GetRequiredService<ILoggerFactory>();
        LogService.InitializeLogService(service);
        return app;
    }    
}
