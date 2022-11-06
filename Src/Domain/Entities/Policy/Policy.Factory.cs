using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Entities.Policy;

public partial class Policy
{
    public Policy()
    {
        // DO NOT REMOVE
    }

    public Policy CreatePolicy()
    {
        return new Policy
        {
            Id = EntityUuid.Generate()
        };
    }
}