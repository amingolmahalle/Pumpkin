using Framework.ValueObjects;

namespace Domain.Framework.Entities.Contracts;

public abstract class GuidCreatableEntity : CreatableEntity<EntityUuid>
{
}