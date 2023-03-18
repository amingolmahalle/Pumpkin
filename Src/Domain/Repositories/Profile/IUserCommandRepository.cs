using System.Linq.Expressions;
using Pumpkin.Domain.Framework.Data.Repositories;

namespace Pumpkin.Domain.Repositories.Profile;

public interface IUserCommandRepository : ICommandRepository<Entities.Profile.User, Guid>
{
    Task<Entities.Profile.User> FindCustomerAsync(Expression<Func<Entities.Profile.User, bool>> predicate, CancellationToken cancellationToken);
}