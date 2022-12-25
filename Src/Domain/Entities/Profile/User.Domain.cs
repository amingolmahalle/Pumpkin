using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;

namespace Pumpkin.Domain.Entities.Profile;

public partial class User: GuidAuditableAggregateRoot
{
    protected override void EnsureReadyState(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}