using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Entities.Profile;
using Pumpkin.Domain.Repositories.Profile;
using Pumpkin.Infrastructure.Contexts;
using Pumpkin.Infrastructure.Framework.Data.Context;
using Pumpkin.Infrastructure.Framework.Data.Repositories;

namespace Pumpkin.Infrastructure.Repositories.Profile;

public class UserCommandRepository : CommandRepository<User, Guid>, IUserCommandRepository
{
    public UserCommandRepository(IHttpContextAccessor accessor, DbContextBase context) : base(accessor, context)
    {
    }

    public async Task<User> FindCustomerAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        => await Entities.FirstOrDefaultAsync(predicate, cancellationToken);
}