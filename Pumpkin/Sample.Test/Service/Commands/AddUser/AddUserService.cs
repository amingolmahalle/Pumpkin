using System.Threading;
using System.Threading.Tasks;
using Sample.Test.Data.Repositories;
using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Commands.AddUser;

namespace Sample.Test.Service.Commands.AddUser
{
    public class AddUserService : IAddUserService
    {
        private IUserRepository UserRepository { get; }

        public AddUserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task ExecuteAsync(AddUserRequest request, CancellationToken cancellationToken)
        {
            AddUserValidation.CheckValidation(request, UserRepository);

            var user = AddUserFactory.Create(request);

            await UserRepository.AddAsync(user, cancellationToken);
        }
    }
}