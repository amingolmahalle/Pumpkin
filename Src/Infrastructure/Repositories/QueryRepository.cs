using Domain.Framework.Entities;
using Domain.Framework.Logging;
using Domain.Framework.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Infrastructure.Contexts;

namespace Pumpkin.Infrastructure.Repositories;

public class QueryRepository<TEntity, TKey> : IQueryRepository<TEntity, TKey>  
    where TEntity : class, IEntity
{
    public ILog Logger { get; }
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> Entities;

    protected QueryRepository(IHttpContextAccessor accessor, DbContextBase context)
    {
        _context = context;
        Entities = context.Set<TEntity>();
        Logger = LogManager.GetLogger<QueryRepository<TEntity, TKey>>();
    }

    public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        
    public async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken)
        => await Entities.FindAsync(id, cancellationToken);
}