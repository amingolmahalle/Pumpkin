namespace Pumpkin.Contract.Domain;

public abstract class EntityBase<TKey> : IEntity
{
    public TKey Id { get; set; }
}

public abstract class IntEntityBase : EntityBase<int>
{
}

public abstract class LongEntityBase : EntityBase<long>
{
}