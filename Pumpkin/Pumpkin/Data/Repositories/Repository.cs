using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Threading;
using Pumpkin.Contract.Domain;
using Pumpkin.Utils.Extensions;

namespace Pumpkin.Data.Repositories
{
    public class Repository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey>
        where TEntity :
        class,
        IEntity<TKey>,
        IAggregateRoot
        where TDbContext :
        DbContext
    {
        private readonly TDbContext _session;

        protected Repository(TDbContext context)
        {
            _session = context;
        }

        private DbSet<TEntity> Set()
        {
            return _session.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query()
        {
            return Set();
        }

        public IQueryable<TU> QueryOn<TU>() where TU : class //, IDataModel
        {
            return _session.Set<TU>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            return await QueryOn<TEntity>().SingleOrDefaultAsync(id.IdentityEquality<TEntity, TKey>(),cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            Set().Add(entity);

            _session.Entry(entity).State = EntityState.Added;
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Set().AddRange(entities);
        }

        public virtual async Task<TEntity> AddOrUpdate(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var existEntity = await Set().Where(predicate).FirstOrDefaultAsync();

            if (existEntity != null)
            {
                Set().Update(existEntity);
            }

            else
            {
                Set().Add(entity);
            }

            return entity;
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            Set().RemoveRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            Set().Remove(entity);

            _session.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Update(TEntity entity)
        {
            _session.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _session.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return await _session.Set<TEntity>()
                    .CountAsync(predicate);

            return await _session.Set<TEntity>()
                .CountAsync();
        }

        public int Save()
        {
            return _session.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _session.SaveChangesAsync(cancellationToken);
        }
    }
}