using Domain.Framework.Entities.Auditable;

namespace Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class AuditableAggregateRoot : AggregateRoot, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}

public abstract class AuditableAggregateRoot<TKey> : AggregateRoot<TKey>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}