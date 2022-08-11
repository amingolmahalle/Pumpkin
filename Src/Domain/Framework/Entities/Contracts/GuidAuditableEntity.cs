using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Domain.Framework.Entities.Contracts;

public abstract class GuidAuditableEntity : AuditableEntity<EntityUuid>
{
}