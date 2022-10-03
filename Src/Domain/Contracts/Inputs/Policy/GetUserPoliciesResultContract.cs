using Pumpkin.Domain.Entities.Policy.Enumerations;

namespace Pumpkin.Domain.Contracts.Inputs.Policy;

public class GetUserPoliciesResultContract
{
    public long PolicyDraftNo { get; set; }
    public string SerialNo { get; set; }
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string Address { get; set; }
    public DateTime? IssuedAt { get; set; }
    public DateTime? StartAt { get; set; }
    public DateTime? ExpireAt { get; set; }
    public bool IsActive { get; set; }
    public PolicyStatus PolicyStatus { get; set; }
}