namespace Pumpkin.Domain.Framework.Entities;

public interface IEntity
{
}
public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}