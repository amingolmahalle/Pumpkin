using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Domain.Events.DomainEvents.Order;

public class OrderPayed : DomainEvent
{
    public string TrackingCode { get; set; }
    public bool IsPaid { get; set; }

    public OrderPayed()
    {
    }
}