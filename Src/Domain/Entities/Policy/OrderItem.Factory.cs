using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class OrderItem
{
    private OrderItem()
    {
        // DO NOT REMOVE
    }

    public OrderItem CreateOrderItem()
    {
        return new OrderItem
        {
            Id = EntityUuid.Generate()
        };
    }
}