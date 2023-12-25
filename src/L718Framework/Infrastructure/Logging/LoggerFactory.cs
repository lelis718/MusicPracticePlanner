using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace L718Framework.Infrastructure.Logging;

/// <summary>
/// Main Factory for Logging Messages
/// </summary>
public class LogService
{
    /// <summary>
    /// Main Method to get Loggers easily
    /// </summary>
    /// <param name="clazz"></param>
    /// <returns></returns>
    public static ILogger GetLogger(Type clazz)
    {

        if (_loggerFactory != null)
        {
            return _loggerFactory.CreateLogger(clazz);
        }
        return NullLogger.Instance;
    }

    private static ILoggerFactory _loggerFactory = null!;
    
    /// <summary>
    /// Singleton reference to the logger service added at configuration level
    /// </summary>
    /// <param name="loggerFactory"></param>
    public static void InitializeLogService(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }
}

