using System;
using Pumpkin.Contract.Domain.Auditable;

namespace Pumpkin.Contract.Domain
{
    public abstract class AuditableEntity<TKey> :
        EntityBase<TKey>,
        ICreatableEntity,
        IModifiableEntity,
        IRemovableEntity
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }

        public DateTime ModifiedAt { get; set; }
        public long ModifiedBy { get; set; }

        public bool Deleted { get; set; }
        public DateTime? RemovedAt { get; set; }
        public long? RemovedBy { get; set; }
    }

    public abstract class IntAuditableEntity : AuditableEntity<int>
    {
    }

    public abstract class LongAuditableEntity : AuditableEntity<long>
    {
    }
}