using System.Threading.Tasks;
using Pumpkin.Contract.Domain;

namespace Sample.Test.Domain.Entity.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByIdAsync(int id);
    }
}