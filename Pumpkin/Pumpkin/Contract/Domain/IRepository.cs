using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Domain
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        Task<TEntity> AddOrUpdate(TEntity entity, Expression<Func<TEntity, bool>> predicate);

        void DeleteRange(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Update(TEntity entity);

        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        int Save();

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}