using System;
using Pumpkin.Contract.Domain.Auditable;

namespace Pumpkin.Contract.Domain
{
    public abstract class CreatableEntity<TKey> :EntityBase<TKey>, ICreatableEntity
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
    }
    public abstract class IntCreatableEntity :CreatableEntity<int>
    {
    }
    public abstract class LongCreatableEntity :CreatableEntity<long>
    {
    }
}