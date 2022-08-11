namespace Pumpkin.Domain.Framework.Logging;

public abstract class LoggingFactoryDefinition
{
    protected internal abstract ILoggerFactory GetLoggingFactory();
}