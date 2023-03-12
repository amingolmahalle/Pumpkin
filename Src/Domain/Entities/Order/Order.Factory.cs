using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class Order
{
    public Order()
    {
        // DO NOT REMOVE
    }

    public Order Create(
        string basketCode,
        string CustomerFirstName,
        string CustomerLastName,
        string CustomerMobileNumber,
        string CustomerNationalCode,
        string CustomerAddress,
        Guid CustomerId,
        decimal totalProductPrice)
    {
        return new Order
        {
            Id = EntityUuid.Generate(),
            BasketCode = basketCode,
            CustomerFirstName = CustomerFirstName,
            CustomerLastName = CustomerLastName,
            CustomerMobileNumber = CustomerMobileNumber,
            CustomerNationalCode = CustomerNationalCode,
            CustomerAddress = CustomerAddress,
            CustomerId = EntityUuid.FromGuid(CustomerId),
            CurrentState = OrderStates.Pending,
            OrderDate = DateTime.Now,
            CancelDeadline = DateTime.Now.AddDays(3),
            IsConfirmed = false,
            IsPaid = false,
            TotalProductPrice = EntityAmount.SetCurrency(totalProductPrice),
        };
    }
}