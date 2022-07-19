using Domain.Framework.Entities.Auditable;

namespace Domain.Framework.Entities.Contracts;

public abstract class AuditableEntity : EntityBase, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}

public abstract class AuditableEntity<TKey> : EntityBase<TKey>, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }

    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}