using Domain.Framework.Logging;

namespace Infrastructure.Framework.Logging.NLog;

public class NLogFactory : LoggingFactoryDefinition
{
    protected override ILoggerFactory GetLoggingFactory()
    {
        return new LoggerFactory();
    }
}