using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void AddRange(IEnumerable<TEntity> entities);

        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void UpdateRange(IEnumerable<TEntity> entities);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void DeleteRange(IEnumerable<TEntity> entities);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    }
}