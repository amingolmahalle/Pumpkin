using Pumpkin.Domain.Events.DomainEvents.Order;

namespace Pumpkin.Domain.Entities.Order;

public partial class OrderItem
{
    protected override void EnsureReadyState(object @event)
    {
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case NewOrderRegistered orderRegistered:
                Policy = new Policy().Create(
                    orderRegistered.CustomerFirstName,
                    orderRegistered.CustomerLastName,
                    orderRegistered.CustomerMobileNumber,
                    orderRegistered.CustomerNationalCode,
                    orderRegistered.CustomerAddress,
                    orderRegistered.CustomerId
                );

                break;
            case OrderConfirmed orderConfirmed:
                DeviceSerialNumber = orderConfirmed.DeviceSerialNumber;
                
                Policy.Apply(@event);
                break;
        }
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}