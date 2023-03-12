using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class OrderItem
{
    public OrderItem()
    {
        // DO NOT REMOVE
    }

    public OrderItem Create(
        string basketItemCode,
        string productCategory,
        string productBrand,
        string productModel,
        decimal productPrice
    )
    {
        return new OrderItem
        {
            Id = EntityUuid.Generate(),
            BasketItemCode = basketItemCode,
            ProductCategory = productCategory,
            ProductBrand = productBrand,
            ProductModel = productModel,
            ProductPrice = EntityAmount.SetCurrency(productPrice),
        };
    }
}