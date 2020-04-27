using System.Threading;
using System.Threading.Tasks;
using Pumpkin.Data.DataServices.DataProviders;
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

        public async Task<User> GetUserByMobileAsync(string mobileNumber, CancellationToken cancellationToken)
        {
            using (var dataProvider = new SqlDataProvider())
            {
                var query = $@"SELECT 
                                                Id,
                                                MobileNumber,
                                                NationalCode,
                                                Email,
                                                BirthDate,
                                                Status
                                        FROM 
                                                User
                                        where 
                                                MobileNumber = {mobileNumber}";

                return await dataProvider.ExecuteSingleRecordQueryCommandAsync<User>(query,cancellationToken);
            }
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