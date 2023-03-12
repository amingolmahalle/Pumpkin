using Pumpkin.Domain.Events.DataTransferObjects.Order;
using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Domain.Events.DomainEvents.Order;

public class OrderConfirmed : DomainEvent
{
    public string BasketItemCode { get; set; }
    public string DeviceSerialNumber { get; set; }
}