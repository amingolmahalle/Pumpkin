using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class Order
{
    private Order()
    {
        // DO NOT REMOVE
    }

    public Order CreateOrder()
    {
        return new Order
        {
            Id = EntityUuid.Generate()
        };
    }
}