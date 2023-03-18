using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Entities.Order;
using Pumpkin.Domain.Framework.Entities;
using Pumpkin.Domain.Repositories.Order;
using Pumpkin.Infrastructure.Contexts;
using Pumpkin.Infrastructure.Framework.Data.Context;
using Pumpkin.Infrastructure.Framework.Data.Repositories;

namespace Pumpkin.Infrastructure.Repositories.Order;

public class OrderQueryRepository : QueryRepository<Domain.Entities.Order.Order, Guid>, IOrderQueryRepository
{
    private readonly DbSet<Policy> _policies;

    protected OrderQueryRepository(IHttpContextAccessor accessor, DbContextBase context) : base(accessor, context)
    {
        _policies = context.Set<Policy>();
    }

    public async Task<Domain.Entities.Order.Order> FindOrderAsync(Expression<Func<Domain.Entities.Order.Order, bool>> predicate, CancellationToken cancellationToken)
        => await TableNoTracking.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<ICollection<Policy>> FindPoliciesAsync(Expression<Func<Policy, bool>> predicate,
        CancellationToken cancellationToken)
        => await _policies
            .Include(p => p.OrderItem)
            .ThenInclude(oi => oi.Order)
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);
}