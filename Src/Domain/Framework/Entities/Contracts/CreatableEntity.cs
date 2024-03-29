using Pumpkin.Domain.Framework.Entities.Auditable;

namespace Pumpkin.Domain.Framework.Entities.Contracts;

public abstract class CreatableEntity : EntityBase, ICreatableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
    
public abstract class CreatableEntity<TKey> : EntityBase<TKey>, ICreatableEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}