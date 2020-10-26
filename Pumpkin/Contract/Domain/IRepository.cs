using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pumpkin.Contract.Domain
{
    public interface IRepository<TEntity, in TKey>
        where TEntity :
        class,
        IEntity<TKey>,
        IAggregateRoot
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    }
}