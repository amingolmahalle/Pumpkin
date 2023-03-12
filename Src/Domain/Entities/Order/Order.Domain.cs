using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Events.DomainEvents.Order;

namespace Pumpkin.Domain.Entities.Order;

public partial class Order
{
    protected override void EnsureReadyState(object @event)
    {
        switch (@event)
        {
            // case OrderCanceld:
            //     if (CurrentState is < BasketStates.Paid or >= BasketStates.Expired)
            //         throw new Dexception(Situation.Make(SitKeys.SchPolicyIsAlreadyCancelled));
            //     // new List<KeyValuePair<string, string>> {new(":پیام:", "بیمه‌نامه‌هایی فریز می‌شوند که مهلت لغو آنها تمام شده و فعال هستند.")});
            //     break;
        }
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case NewOrderRegistered orderRegistered:
                OrderItems ??= new List<OrderItem>();
                foreach (var orderItem in orderRegistered.OrderItems)
                {
                    OrderItems.Add(new OrderItem().Create(
                        orderItem.BasketItemCode,
                        orderItem.ProductCategory,
                        orderItem.ProductBrand,
                        orderItem.ProductModel,
                        orderItem.ProductPrice
                    ));
                }

                OrderItems.ForEach(item =>
                {
                    item.Order = this;
                    item.Apply(@event);
                });

                // orderRegistered.Payload.Add("CurrentState", CurrentState);
                break;
            case OrderPayed orderPayed:
                PaymentTrackingCode = orderPayed.TrackingCode;
                IsPaid = orderPayed.IsPaid;
                PaymentState = orderPayed.IsPaid ? PaymentStates.Succeed : PaymentStates.Failed;
                CurrentState = OrderStates.Paid;
                break;
            case OrderConfirmed orderConfirmed:
                var selectedOrderItem = OrderItems.FirstOrDefault(oi => oi.BasketItemCode == orderConfirmed.BasketItemCode);
                if (selectedOrderItem is not null)
                {
                    selectedOrderItem.Apply(@event);

                    if (OrderItems
                        .All(x => !string.IsNullOrWhiteSpace(x.DeviceSerialNumber)))
                    {
                        CurrentState = OrderStates.Confirmed;
                        IsConfirmed = true;
                    }
                }

                break;
        }
    }

    protected override void EnsureValidState()
    {
        var isValid =
            Id != null &&
            CurrentState switch
            {
                OrderStates.Pending => true,
                OrderStates.Paid => PaymentState == PaymentStates.Succeed && IsPaid,
                OrderStates.Cancelled => true,

                _ => false
            };
    }
}