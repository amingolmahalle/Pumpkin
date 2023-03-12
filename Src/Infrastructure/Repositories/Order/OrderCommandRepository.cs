using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Repositories.Order;
using Pumpkin.Infrastructure.Contexts;

namespace Pumpkin.Infrastructure.Repositories.Order;

public class OrderCommandRepository : CommandRepository<Domain.Entities.Order.Order, Guid>, IOrderCommandRepository
{
    public OrderCommandRepository(IHttpContextAccessor accessor, DbContextBase context) : base(accessor, context)
    {
    }

    public async Task<Domain.Entities.Order.Order> FindOrderAsync(Expression<Func<Domain.Entities.Order.Order, bool>> predicate, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(predicate, cancellationToken);
}