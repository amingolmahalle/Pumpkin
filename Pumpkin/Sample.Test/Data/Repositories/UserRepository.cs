using System.Threading.Tasks;
using Pumpkin.Data;
using Pumpkin.Data.Repositories;
using Sample.Test.Domain.Entity;

namespace Sample.Test.Data.Repositories
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        protected UserRepository(DatabaseContext context) : base(context)
        {
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}