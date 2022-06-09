namespace Pumpkin.Contract.Events;

public abstract class DomainEvent
{
    public string ExchangeName { get; set; }
    public string[] Routes { get; set; } = Array.Empty<string>();
    public Dictionary<string, object> Payload { get; set; } = new();
}