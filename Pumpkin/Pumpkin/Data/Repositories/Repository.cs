using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Pumpkin.Contract.Domain;

namespace Pumpkin.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected DatabaseContext Session;

        // ILog log = LogManager.GetLogger("RepositoryBase");

        protected Repository(DatabaseContext context)
        {
            Session = context;
        }

        protected DbSet<TEntity> Set()
        {
            return Session.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Set().Add(entity);
            Session.Entry(entity).State = EntityState.Added;
            Session.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Set().AddAsync(entity);
            Session.Entry(entity).State = EntityState.Added;
            await Session.SaveChangesAsync();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Set().AddRange(entities);

            Session.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Set().AddRangeAsync(entities);

            await Session.SaveChangesAsync();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Session.Entry(entity).State = EntityState.Modified;
            }

            Session.SaveChanges();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Session.Entry(entity).State = EntityState.Modified;
            }

            await Session.SaveChangesAsync();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            Set().RemoveRange(entities);

            Session.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            Set().RemoveRange(entities);

            await Session.SaveChangesAsync();
        }

        public virtual void Delete(TEntity entity)
        {
            Set().Remove(entity);

            Session.Entry(entity).State = EntityState.Deleted;

            Session.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            Set().Remove(entity);

            Session.Entry(entity).State = EntityState.Deleted;

            await Session.SaveChangesAsync();
        }

        public virtual void Update(TEntity entity)
        {
            Session.Entry(entity).State = EntityState.Modified;

            Session.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            Session.Entry(entity).State = EntityState.Modified;

            await Session.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Session.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await Session.Set<TEntity>()
                    .CountAsync(predicate);

            return await Session.Set<TEntity>()
                .CountAsync();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return Set();
        }

        public IQueryable<TU> QueryOn<TU>() where TU : class, IDataModel
        {
            return Session.Set<TU>();
        }
    }
}