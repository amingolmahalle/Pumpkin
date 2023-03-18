using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Entities.Auditable;
using Pumpkin.Domain.Framework.Specifications;

namespace Pumpkin.Domain.Framework.Data.Repositories;

public interface ICreatableCommandRepository<TEntity, TKey> : ICommandRepositoryBase 
    where TEntity : class, ICreatableEntity
{
    DbSet<TEntity> Entities { get; }
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void PhysicalRemove(TKey id);
    void PhysicalRemove(TEntity entity);
    IEnumerable<TEntity> FindWithSpecificationPattern(ISpecification<TEntity> specification = null);
}