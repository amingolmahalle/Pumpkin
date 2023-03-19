using Pumpkin.Domain.Framework.Entities.Contracts;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class OrderItem : GuidAuditableEntity
{
    public string BasketItemCode { get; set; }
    public string ProductCategory { get; set; } 
    public string ProductBrand { get; set; }
    public string ProductModel { get; set; }
    public string DeviceSerialNumber { get; set; }
    public EntityAmount ProductPrice { get; set; }
    public EntityUuid OrderId { get; set; }

    #region :: REPLATIONS ::
    public Order Order { get; set; }
    public Policy Policy { get; set; }

    #endregion
}