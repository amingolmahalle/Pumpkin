using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Pumpkin.Contract.Domain
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void UpdateRange(IEnumerable<T> entities);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        void DeleteRange(IEnumerable<T> entities);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        void Delete(T entity);

        Task DeleteAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        Task<long> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}