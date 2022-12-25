using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace Pumpkin.Domain.Entities.Profile;

public partial class User: GuidAuditableAggregateRoot
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string Address { get; set; }
    public bool Gender { get; set; }
}