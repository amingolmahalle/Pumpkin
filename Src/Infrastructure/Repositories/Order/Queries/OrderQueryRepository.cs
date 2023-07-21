using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Entities.Order;
using Pumpkin.Domain.Repositories.Order;
using Pumpkin.Infrastructure.Framework.Data.Context;
using Pumpkin.Infrastructure.Framework.Data.Repositories;
using Pumpkin.Infrastructure.Repositories.Order.Queries.Specifications;

namespace Pumpkin.Infrastructure.Repositories.Order.Queries;

public class OrderQueryRepository : QueryRepository<Domain.Entities.Order.Order, Guid>, IOrderQueryRepository
{
    private readonly DbSet<Policy> _policies;

    public OrderQueryRepository(QueryDbContext context) : base(context)
    {
        _policies = context.Set<Policy>();
    }

    public async Task<ICollection<Policy>> FindPoliciesAsync(Guid customerId, CancellationToken cancellationToken)
    {
       var orders= await ApplySpecification(new FindCustomerPoliciesByCustomerIdSpecification(customerId))
            .AsNoTracking()
            .ToListAsync(cancellationToken);

       List<Policy> policies = (from o in orders from oi in o.OrderItems select oi.Policy).ToList();
       
       return policies;
    }
}