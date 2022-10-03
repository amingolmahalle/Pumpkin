using System.Linq.Expressions;
using Pumpkin.Domain.Framework.Repositories;

namespace Pumpkin.Domain.Repositories.Policy;

public interface IPolicyCommandRepository : ICommandRepository<Entities.Policy.Policy, Guid>
{
    Task<Entities.Policy.Policy> GetPolicyAsync(Expression<Func<Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken);
}