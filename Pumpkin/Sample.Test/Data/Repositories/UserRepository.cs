using System.Threading;
using System.Threading.Tasks;
using Pumpkin.Data.Repositories;
using Sample.Test.Domain.Entity.UserAggregate;

namespace Sample.Test.Data.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(id, cancellationToken);
        }

        public void AddUser(User user)
        {
            Add(user);
        }

        public void EditUser(User user)
        {
            Update(user);
        }
    }
}