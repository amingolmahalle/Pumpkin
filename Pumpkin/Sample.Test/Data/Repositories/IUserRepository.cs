using System.Threading.Tasks;
using Pumpkin.Contract.Domain;
using Sample.Test.Domain.Entity;

namespace Sample.Test.Data.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetByIdAsync(int id);
    }
}