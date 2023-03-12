using System.Linq.Expressions;
using Pumpkin.Domain.Framework.Repositories;

namespace Pumpkin.Domain.Repositories.Order;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order.Order, Guid>
{
    Task<Entities.Order.Order> FindOrderAsync(Expression<Func<Entities.Order.Order, bool>> predicate, CancellationToken cancellationToken);
}