using LogManager = NLog.LogManager;
using Domain.Framework.Logging;

namespace Infrastructure.Framework.Logging.NLog;

internal class LoggerFactory : ILoggerFactory
{
    public ILog GetLogger(string name)
    {
        var logger = LogManager.GetLogger(name);

        return new Logger(logger);
    }
}