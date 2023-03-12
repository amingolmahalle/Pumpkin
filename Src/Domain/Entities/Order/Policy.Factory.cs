using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Order;

public partial class Policy
{
    public Policy()
    {
        // DO NOT REMOVE
    }

    public Policy Create(
        string customerFirstName,
        string customerLastname,
        string customerMobileNumber,
        string customerNationalCode,
        string customerAddress,
        Guid customerId)
    {
        return new Policy
        {
            Id = EntityUuid.Generate(),
            PolicyNumber = 1000000, //generate from 1 million
            CustomerFirstName = customerFirstName,
            CustomerLastName = customerLastname,
            CustomerMobileNumber = customerMobileNumber,
            CustomerNationalCode = customerNationalCode,
            CustomerAddress = customerAddress,
            CustomerId = EntityUuid.FromGuid(customerId),
            IsActive = false,
            CurrentState = PolicyStates.Pending,
        };
    }
}