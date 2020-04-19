using System.Threading;
using System.Threading.Tasks;
using Pumpkin.Contract.Domain;

namespace Sample.Test.Domain.Entity.UserAggregate
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);

        void AddUser(User user);

        void EditUser(User user);
    }
}