using Microsoft.Extensions.Logging;
using NippoControlSystem.ApplicationService.Interfaces;

namespace NippoControlSystem.Infrastructure.Services;

public class AppLogger<TCategoryName> : IAppLogger<TCategoryName>
{
    private readonly ILogger<TCategoryName> _logger;

    public AppLogger(ILogger<TCategoryName> logger)
    {
        _logger = logger;
    }

    public void LogInfo(string message, params object?[] args)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(message, args);
        }
    }

    public void LogWarning(string message, params object?[] args)
    {
        if (_logger.IsEnabled(LogLevel.Warning))
        {
            _logger.LogWarning(message, args);
        }
    }

    public void LogError(Exception? exception, string message, params object?[] args)
    {
        if (_logger.IsEnabled(LogLevel.Error))
        {
            _logger.LogError(exception, message, args);
        }
    }
}