using Pumpkin.Domain.Framework.Entities;
using Pumpkin.Domain.Framework.Specifications;

namespace Pumpkin.Domain.Framework.Data.Repositories;

public interface IQueryRepository<TEntity, TKey> : IRepository
    where TEntity : class, IEntity
{
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);
    IEnumerable<TEntity> FindWithSpecificationPattern(ISpecification<TEntity> specification = null);
}