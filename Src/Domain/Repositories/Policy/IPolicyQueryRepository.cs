using System.Linq.Expressions;
using Pumpkin.Domain.Framework.Repositories;

namespace Pumpkin.Domain.Repositories.Policy;

public interface IPolicyQueryRepository : IQueryRepository<Entities.Policy.Policy, Guid>
{
    Task<Entities.Policy.Policy> GetPolicyAsync(Expression<Func<Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken);
    Task<ICollection<Entities.Policy.Policy>> GetPoliciesAsync(Expression<Func<Domain.Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken);
}