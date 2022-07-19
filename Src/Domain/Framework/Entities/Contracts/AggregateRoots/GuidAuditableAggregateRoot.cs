using Framework.ValueObjects;

namespace Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class GuidAuditableAggregateRoot : AuditableAggregateRoot<EntityUuid>
{
}