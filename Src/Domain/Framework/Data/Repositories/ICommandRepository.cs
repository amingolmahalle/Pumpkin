using Pumpkin.Domain.Framework.Entities.Auditable;

namespace Pumpkin.Domain.Framework.Data.Repositories;

public interface ICommandRepository<TEntity, TKey> : ICreatableCommandRepository<TEntity, TKey> 
    where TEntity : class, IAuditableEntity
{
    void Modify(TEntity entity);
    void Remove(TKey id);
    void Remove(TEntity entity);
    void Restore(TEntity entity);
}