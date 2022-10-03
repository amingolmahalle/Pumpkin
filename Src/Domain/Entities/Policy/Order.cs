using Pumpkin.Domain.Entities.Policy.Enumerations;
using Pumpkin.Domain.Framework.Entities.Contracts;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class Order : GuidAuditableEntity
{
    public string BasketCode { get; set; }
    public PolicyStatus PolicyStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CancelDeadline { get; set; }
    public bool IsConfirmed { get; set; }
    public bool IsPaid { get; set; }
    public EntityAmount TotalProductPrice { get; set; }
}