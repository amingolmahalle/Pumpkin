using LogManager = NLog.LogManager;
using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Infrastructure.Framework.Logging.NLog;

internal class LoggerFactory : ILoggerFactory
{
    public ILog GetLogger(string name)
    {
        var logger = LogManager.GetLogger(name);

        return new Logger(logger);
    }
}