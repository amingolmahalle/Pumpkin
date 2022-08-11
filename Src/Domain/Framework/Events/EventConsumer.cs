using Domain.Framework.Logging;

namespace Domain.Framework.Events;

public abstract class EventConsumer
{
    public ILog Logger { get; }
    public EventConsumer()
    {
        Logger = LogManager.GetLogger<EventConsumer>();
    }
}