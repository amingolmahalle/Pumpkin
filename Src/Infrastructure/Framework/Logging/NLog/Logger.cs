using Domain.Framework.Logging;
using NLog;

namespace Infrastructure.Framework.Logging.NLog;

internal class Logger : ILog
{
    private readonly ILogger _logger;

    public Logger(ILogger logger)
    {
        _logger = logger;
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Debug(string message, Exception exception)
    {
        _logger.Debug(message, exception);
    }

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Info(string message, Exception exception)
    {
        _logger.Info(message, exception);
    }

    public void Warn(string message)
    {
        _logger.Warn(message);
    }

    public void Warn(string message, Exception exception)
    {
        _logger.Warn(message, exception);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(string message, Exception exception)
    {
        _logger.Error(message, exception);
    }

    public void Fatal(string message)
    {
        _logger.Fatal(message);
    }

    public void Fatal(string message, Exception exception)
    {
        _logger.Fatal(message, exception);
    }

    public bool IsDebugEnabled => _logger.IsDebugEnabled;
        
    public bool IsInfoEnabled => _logger.IsInfoEnabled;
        
    public bool IsWarnEnabled => _logger.IsWarnEnabled;
        
    public bool IsErrorEnabled => _logger.IsErrorEnabled;
        
    public bool IsFatalEnabled => _logger.IsFatalEnabled;
}