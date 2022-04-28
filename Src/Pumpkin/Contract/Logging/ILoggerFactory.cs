
namespace Pumpkin.Contract.Logging;

public interface ILoggerFactory
{
    ILog GetLogger(string name);
}