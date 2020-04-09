using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Threading;
using Pumpkin.Contract.Domain;

namespace Pumpkin.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly DatabaseContext _session;

        // ILog log = LogManager.GetLogger("RepositoryBase");

        protected Repository(DatabaseContext context)
        {
            _session = context;
        }

        private DbSet<TEntity> Set()
        {
            return _session.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            Set().Add(entity);
            _session.Entry(entity).State = EntityState.Added;
            _session.SaveChanges();
        }

        public virtual async Task AddAsync(TEntity entity,CancellationToken cancellationToken)
        {
            await Set().AddAsync(entity,cancellationToken);
            _session.Entry(entity).State = EntityState.Added;
            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Set().AddRange(entities);

            _session.SaveChanges();
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken)
        {
            await Set().AddRangeAsync(entities,cancellationToken);

            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _session.Entry(entity).State = EntityState.Modified;
            }

            _session.SaveChanges();
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                _session.Entry(entity).State = EntityState.Modified;
            }

            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            Set().RemoveRange(entities);

            _session.SaveChanges();
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities,CancellationToken cancellationToken)
        {
            Set().RemoveRange(entities);

            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual void Delete(TEntity entity)
        {
            Set().Remove(entity);

            _session.Entry(entity).State = EntityState.Deleted;

            _session.SaveChanges();
        }

        public virtual async Task DeleteAsync(TEntity entity,CancellationToken cancellationToken)
        {
            Set().Remove(entity);

            _session.Entry(entity).State = EntityState.Deleted;

            await _session.SaveChangesAsync(cancellationToken);
        }

        public virtual void Update(TEntity entity)
        {
            _session.Entry(entity).State = EntityState.Modified;

            _session.SaveChanges();
        }

        public virtual async Task UpdateAsync(TEntity entity,CancellationToken cancellationToken)
        {
            _session.Entry(entity).State = EntityState.Modified;

            await _session.SaveChangesAsync(cancellationToken);
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

        public virtual IQueryable<TEntity> Query()
        {
            return Set();
        }

        public IQueryable<TU> QueryOn<TU>() where TU : class //, IDataModel
        {
            return _session.Set<TU>();
        }
    }
}