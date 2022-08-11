using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class GuidCreatableAggregateRoot : CreatableAggregateRoot<EntityUuid>
{
}