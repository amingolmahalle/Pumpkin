using Pumpkin.Domain.Entities.Policy.Enumerations;
using Pumpkin.Domain.Framework.Entities.Contracts;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class OrderItem : GuidAuditableEntity
{
    public string BasketItemCode { get; set; }
    public string ProductCategory { get; set; } 
    public string ProductBrand { get; set; }
    public string ProductModel { get; set; }
    public int Count { get; set; }
    public PolicyStatus PolicyStatus { get; set; }
    public DateTime? ConfirmAt { get; set; }
    public DateTime? PaidAt { get; set; }
    public EntityAmount ProductPrice { get; set; }

    public EntityUuid OrderId { get; set; }

    #region :: REPLATIONS ::

    public Order Order { get; set; }

    #endregion
}