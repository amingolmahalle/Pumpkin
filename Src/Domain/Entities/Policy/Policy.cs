using Pumpkin.Domain.Entities.Policy.Enumerations;
using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class Policy : GuidAuditableAggregateRoot
{
    public long PolicyNumber { get; set; }
    public string SerialNumber { get; set; }
    public string BuyerFirstName { get; set; }
    public string BuyerLastname { get; set; }
    public string BuyerMobileNumber { get; set; }
    public string BuyerNationalCode { get; set; }
    public string BuyerAddress { get; set; }
    public EntityUuid BuyerId { get; set; }
    public DateTime? IssuedAt { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? ExpireAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsPaid { get; set; }
    public PolicyStatus PolicyStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    public EntityUuid OrderItemId { get; set; }

    #region :: REPLATIONS ::

    public OrderItem OrderItem { get; set; }

    #endregion
}