using Microsoft.EntityFrameworkCore;
using Pumpkin.Common.Helpers;
using Pumpkin.Contract.Domain;

namespace Pumpkin.Data.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity :
    class,
    IEntity,
    IAggregateRoot
{
    private readonly DatabaseContext _session;

    public DbSet<TEntity> Entities { get; }

    public Repository(DatabaseContext context)
    {
        _session = context;
        Entities = _session.Set<TEntity>(); // City => Cities
    }

    public virtual IQueryable<TEntity> Table => Entities;

    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();

    public virtual ValueTask<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
    {
        return Entities.FindAsync(ids, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entity, nameof(entity));
        await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entities, nameof(entities));
        await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entity, nameof(entity));
        Entities.Update(entity);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entities, nameof(entities));
        Entities.UpdateRange(entities);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entity, nameof(entity));
        Entities.Remove(entity);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
    {
        Helpers.NotNull(entities, nameof(entities));
        Entities.RemoveRange(entities);
        if (saveNow)
            await _session.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}