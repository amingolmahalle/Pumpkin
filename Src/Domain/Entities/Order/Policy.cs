using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.Entities.Contracts;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class Policy : GuidAuditableEntity
{
    public long PolicyNumber { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobileNumber { get; set; }
    public string CustomerNationalCode { get; set; }
    public string CustomerAddress { get; set; }
    public EntityUuid CustomerId { get; set; }
    public DateTime? IssuedAt { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? ExpireAt { get; set; }
    public bool IsActive { get; set; }
    public PolicyStates CurrentState { get; set; }
    
    public EntityUuid OrderItemId { get; set; }

    #region :: REPLATIONS ::

    public OrderItem OrderItem { get; set; }

    #endregion
}