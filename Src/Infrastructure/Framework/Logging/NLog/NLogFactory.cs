using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Infrastructure.Framework.Logging.NLog;

public class NLogFactory : LoggingFactoryDefinition
{
    protected override ILoggerFactory GetLoggingFactory()
    {
        return new LoggerFactory();
    }
}