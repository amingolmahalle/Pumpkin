namespace Domain.Framework.Logging;

public interface ILoggerFactory
{
    ILog GetLogger(string name);
}