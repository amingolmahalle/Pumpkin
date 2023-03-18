using System.Linq.Expressions;
using Pumpkin.Domain.Entities.Order;
using Pumpkin.Domain.Framework.Data.Repositories;
using Pumpkin.Domain.Framework.Entities;

namespace Pumpkin.Domain.Repositories.Order;

public interface IOrderQueryRepository : IQueryRepository<Entities.Order.Order, Guid>
{
    Task<Entities.Order.Order> FindOrderAsync(Expression<Func<Entities.Order.Order, bool>> predicate, CancellationToken cancellationToken);
    Task<ICollection<Policy>> FindPoliciesAsync(Expression<Func<Policy, bool>> predicate, CancellationToken cancellationToken);
}