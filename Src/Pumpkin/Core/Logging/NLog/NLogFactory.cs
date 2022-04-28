using Pumpkin.Contract.Logging;

namespace Pumpkin.Core.Logging.NLog;

public class NLogFactory : LoggingFactoryDefinition
{
    protected internal override ILoggerFactory GetLoggingFactory()
    {
        return new LoggerFactory();
    }
}