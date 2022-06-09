using Pumpkin.Contract.Logging;

namespace Pumpkin.Contract.Events;

public abstract class EventConsumer
{
    public ILog Logger { get; }
    public EventConsumer()
    {
        Logger = LogManager.GetLogger<EventConsumer>();
    }
}