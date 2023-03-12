using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class Order : GuidAuditableAggregateRoot
{
    public string BasketCode { get; set; }
    public string PaymentTrackingCode { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobileNumber { get; set; }
    public string CustomerNationalCode { get; set; }
    public string CustomerAddress { get; set; }
    public EntityUuid CustomerId { get; set; }
    public OrderStates CurrentState { get; set; }
    public PaymentStates PaymentState { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CancelDeadline { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsPaid { get; set; }
    public EntityAmount TotalProductPrice { get; set; }

    #region :: REPLATIONS ::
    
    public List<OrderItem> OrderItems { get; set; }

    #endregion
}