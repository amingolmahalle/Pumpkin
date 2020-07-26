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
            return await GetByIdAsync(cancellationToken, id);
        }

        public async Task<User> GetUserByMobileAsync(string mobileNumber, CancellationToken cancellationToken)
        {
            using var dataProvider = new SqlDataProvider();
            var query = $@"SELECT 
                                                Id,
                                                Fullname,
                                                MobileNumber,
                                                NationalCode,
                                                Email,
                                                BirthDate,
                                                Status
                                        FROM 
                                                Users
                                        WHERE 
                                                MobileNumber = {mobileNumber}";

            return await dataProvider.ExecuteSingleRecordQueryCommandAsync<User>(query, cancellationToken);
        }

        public async Task AddUserAsync(User user, CancellationToken cancellationToken)
        {
            await AddAsync(user, cancellationToken);
        }

        public async Task EditUserAsync(User user, CancellationToken cancellationToken)
        {
            await UpdateAsync(user, cancellationToken);
        }
    }
}