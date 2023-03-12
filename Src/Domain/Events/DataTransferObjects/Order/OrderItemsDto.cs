namespace Pumpkin.Domain.Events.DataTransferObjects.Order;

public class OrderItemsDto
{
    public string BasketItemCode { get; set; }
    public string ProductCategory { get; set; }
    public string ProductBrand { get; set; }
    public string ProductModel { get; set; }
    public decimal ProductPrice { get; set; }
}