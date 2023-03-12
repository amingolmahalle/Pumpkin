using Pumpkin.Domain.Entities.Order.Enumerations;

namespace Pumpkin.Domain.Contracts.Inputs.Order;

public class GetCustomerPoliciesResultContract
{
    public long PolicyDraftNo { get; set; }
    public string SerialNo { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string Address { get; set; }
    public DateTime? IssuedAt { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? ExpireAt { get; set; }
    public bool IsActive { get; set; }
    public OrderStates PolicyStatus { get; set; }
}