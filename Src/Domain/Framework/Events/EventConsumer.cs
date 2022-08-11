using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Events;

public abstract class EventConsumer
{
    public ILog Logger { get; }
    public EventConsumer()
    {
        Logger = LogManager.GetLogger<EventConsumer>();
    }
}