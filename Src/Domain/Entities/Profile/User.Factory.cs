using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Profile;

public partial class User
{
    public User()
    {
        // DO NOT REMOVE
    }

    public User Create(
        string firstname,
        string lastName,
        string mobileNumber,
        string nationalCode,
        string address,
        bool? gender)
    {
        return new User
        {
            Id = EntityUuid.Generate(),
            FirstName = firstname,
            LastName = lastName,
            MobileNumber = mobileNumber,
            NationalCode = nationalCode,
            Address = address,
            Gender = gender,
        };
    }
}