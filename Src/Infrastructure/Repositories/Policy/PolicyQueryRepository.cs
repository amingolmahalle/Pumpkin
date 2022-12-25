using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Repositories.Policy;
using Pumpkin.Infrastructure.Contexts;

namespace Pumpkin.Infrastructure.Repositories.Policy;

public class PolicyQueryRepository : QueryRepository<Domain.Entities.Policy.Policy, Guid>, IPolicyQueryRepository
{
    protected PolicyQueryRepository(IHttpContextAccessor accessor, DbContextBase context) : base(accessor, context)
    {
    }

    public async Task<Domain.Entities.Policy.Policy> FindPolicyAsync(Expression<Func<Domain.Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken)
        => await TableNoTracking.FirstOrDefaultAsync(predicate, cancellationToken);

    public async Task<ICollection<Domain.Entities.Policy.Policy>> FindPoliciesAsync(Expression<Func<Domain.Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken)
        => await TableNoTracking
            .Where(predicate)
            .ToListAsync(cancellationToken);
}