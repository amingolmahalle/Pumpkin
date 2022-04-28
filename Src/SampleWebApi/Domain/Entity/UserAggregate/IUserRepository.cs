using Pumpkin.Contract.Domain;

namespace SampleWebApi.Domain.Entity.UserAggregate;

public interface IUserRepository: IRepository<User, int>
{
    Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);

    Task<User> GetUserByMobileAsync(string mobileNumber, CancellationToken cancellationToken);

    Task AddUserAsync(User user, CancellationToken cancellationToken);

    Task EditUserAsync(User user, CancellationToken cancellationToken);
}