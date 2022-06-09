namespace Pumpkin.Contract.Events;

public class BusEvent
{
    public string ExchangeName { get; set; } = null!;
    public string[] Routes { get; set; } = Array.Empty<string>();
    public string EventType { get; set; } = null!;
    public int Retry { get; set; } = 1;
    public string RetryType { get; set; }
    public string Payload { get; set; } = null!;
}