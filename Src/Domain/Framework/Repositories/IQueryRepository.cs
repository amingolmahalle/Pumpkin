using Domain.Framework.Entities;
using Framework.Repositories;

namespace Domain.Framework.Repositories;

public interface IQueryRepository<TEntity, TKey> : IRepository
    where TEntity : class, IEntity
{
    Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken);
}