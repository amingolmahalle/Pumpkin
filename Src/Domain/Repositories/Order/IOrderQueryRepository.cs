using Pumpkin.Domain.Framework.Data.Repositories;

namespace Pumpkin.Domain.Repositories.Order;

public interface IOrderQueryRepository : IQueryRepository<Entities.Order.Order, Guid>
{
    Task<ICollection<Entities.Order.Policy>> FindPoliciesAsync(Guid customerId, CancellationToken cancellationToken);
}