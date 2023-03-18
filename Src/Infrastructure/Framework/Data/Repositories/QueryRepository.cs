using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Data.Repositories;
using Pumpkin.Domain.Framework.Entities;
using Pumpkin.Domain.Framework.Logging;
using Pumpkin.Domain.Framework.Specifications;
using Pumpkin.Infrastructure.Framework.Data.Context;
using Pumpkin.Infrastructure.Framework.Data.Specifications;

namespace Pumpkin.Infrastructure.Framework.Data.Repositories;

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

    public IEnumerable<TEntity> FindWithSpecificationPattern(ISpecification<TEntity> specification = null)
        => SpecificationEvaluator<TEntity>.GetQuery(Entities.AsQueryable(), specification);
}