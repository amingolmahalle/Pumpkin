namespace Pumpkin.Contract.Logging;

public abstract class LoggingFactoryDefinition
{
    protected internal abstract ILoggerFactory GetLoggingFactory();
}