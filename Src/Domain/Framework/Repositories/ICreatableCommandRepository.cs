using Domain.Framework.Entities.Auditable;
using Microsoft.EntityFrameworkCore;

namespace Domain.Framework.Repositories;

public interface ICreatableCommandRepository<TEntity, TKey> : ICommandRepositoryBase 
    where TEntity : class, ICreatableEntity
{
    DbSet<TEntity> Entities { get; }
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void PhysicalRemove(TKey id);
    void PhysicalRemove(TEntity entity);
}