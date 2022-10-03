using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Repositories.Policy;
using Pumpkin.Infrastructure.Contexts;

namespace Pumpkin.Infrastructure.Repositories.Policy;

public class PolicyCommandRepository : CommandRepository<Domain.Entities.Policy.Policy, Guid>, IPolicyCommandRepository
{
    public PolicyCommandRepository(IHttpContextAccessor accessor, DbContextBase context) : base(accessor, context)
    {
    }

    public async Task<Domain.Entities.Policy.Policy> GetPolicyAsync(Expression<Func<Domain.Entities.Policy.Policy, bool>> predicate, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(predicate, cancellationToken);
}