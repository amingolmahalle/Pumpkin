using Pumpkin.Contract.Logging;

namespace Pumpkin.Contract.Events;

public interface IEventConsumer
{
    public bool Enabled { get; }

    public string Route { get; }

    public string BusName { get; }

    public string EventType { get; }

    public int MaxRetry { get; set; }

    public TimeSpan RetryWait { get; set; }

    public ILog Logger { get; }

    Task<bool> Handle(int @try, string payload);
}