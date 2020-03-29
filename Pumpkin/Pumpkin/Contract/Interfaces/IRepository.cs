using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreService.Framework.Contracts.Domain
{
    public interface IRepository<T> : IRepository<T,int> where T : class,IEntity<T>, IAggregateRoot
    {
    }

    public interface IRepository<T, TId> : IEntity<T, TId>, IAggregateRoot where T : class
    {
        TId Add(T entity);
        
        Task<TId> AddAsync(T entity);
        
        void AddRang(IEnumerable<T> entities);
        
        Task AddRangeAsync(IEnumerable<T> entities);
        
        void UpdateRang(IEnumerable<T> entities);
        
        Task UpdateRangeAsync(IEnumerable<T> entities);
        
        void DeleteRang(IEnumerable<T> entities);
        
        Task DeleteRangeAsync(IEnumerable<T> entities);
        
        void Delete(T entity);
        
        Task DeleteAsync(T entity);
        
        void Delete(TId id);
        
        Task DeleteAsync(TId id);
        
        void Update(T entity);
        
        Task UpdateAsync(T entity);
        
        void SaveOrUpdate(TId id,Action<T> add,Action<T> update);
        
        Task SaveOrUpdateAsync(TId id, Func<T, Task> add, Func<T, Task> update);
        
        long Count(Expression<Func<T, bool>> predicate = null);
    }
}
