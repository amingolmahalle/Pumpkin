using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Pumpkin.Contract.Domain;

namespace Pumpkin.Data.Repositories
{
    public abstract class Repository<T> : IRepository<T>
        where T : class
    {
        protected DatabaseContext Session;

        // ILog log = LogManager.GetLogger("RepositoryBase");

        protected Repository(DatabaseContext context)
        {
            Session = context;
        }

        protected DbSet<T> Set()
        {
            return Session.Set<T>();
        }

        public virtual void Add(T entity)
        {
            Set().Add(entity);
            Session.Entry(entity).State = EntityState.Added;
            Session.SaveChanges();
        }

        public virtual async Task AddAsync(T entity)
        {
            await Set().AddAsync(entity);
            Session.Entry(entity).State = EntityState.Added;
            await Session.SaveChangesAsync();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Set().AddRange(entities);

            Session.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Set().AddRangeAsync(entities);

            await Session.SaveChangesAsync();
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Session.Entry(entity).State = EntityState.Modified;
            }

            Session.SaveChanges();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Session.Entry(entity).State = EntityState.Modified;
            }

            await Session.SaveChangesAsync();
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            Set().RemoveRange(entities);

            Session.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            Set().RemoveRange(entities);

            await Session.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            Set().Remove(entity);

            Session.Entry(entity).State = EntityState.Deleted;

            Session.SaveChanges();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            Set().Remove(entity);

            Session.Entry(entity).State = EntityState.Deleted;

            await Session.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            Session.Entry(entity).State = EntityState.Modified;

            Session.SaveChanges();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            Session.Entry(entity).State = EntityState.Modified;

            await Session.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Session.Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
                return await Session.Set<T>()
                    .CountAsync(predicate);

            return await Session.Set<T>()
                .CountAsync();
        }

        public virtual IQueryable<T> Query()
        {
            return Set();
        }

        public IQueryable<TU> QueryOn<TU>() where TU : class, IDataModel
        {
            return Session.Set<TU>();
        }
    }
}