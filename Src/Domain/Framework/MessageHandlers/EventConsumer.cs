using Domain.Framework.Logging;

namespace Domain.Framework.MessageHandlers;

public abstract class EventConsumer
{
    public ILog Logger { get; }
    public EventConsumer()
    {
        Logger = LogManager.GetLogger<EventConsumer>();
    }
}