using Pumpkin.Domain.Events.DomainEvents.Profile;
using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace Pumpkin.Domain.Entities.Profile;

public partial class User
{
    protected override void EnsureReadyState(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case NewCustomerCreated newCustomer:
                FirstName = !string.IsNullOrWhiteSpace(newCustomer.FirstName) && FirstName != newCustomer.FirstName ? newCustomer.FirstName : FirstName;
                LastName = !string.IsNullOrWhiteSpace(newCustomer.LastName) && LastName != newCustomer.LastName ? newCustomer.LastName : LastName;
                NationalCode = !string.IsNullOrWhiteSpace(newCustomer.NationalCode) && NationalCode != newCustomer.NationalCode ? newCustomer.NationalCode : NationalCode;
                Address = !string.IsNullOrWhiteSpace(newCustomer.Address) && Address != newCustomer.Address ? newCustomer.Address : Address;
                Gender = newCustomer.Gender.HasValue && Gender != newCustomer.Gender ? newCustomer.Gender : Gender;
                break;
        }
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}