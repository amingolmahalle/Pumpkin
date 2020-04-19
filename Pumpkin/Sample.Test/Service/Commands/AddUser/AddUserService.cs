using System.Threading;
using System.Threading.Tasks;
using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Commands.AddUser;

namespace Sample.Test.Service.Commands.AddUser
{
    public class AddUserService : IAddUserService
    {
        private readonly IUserRepository _userRepository;

        public AddUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(AddUserRequest request, CancellationToken cancellationToken)
        {
            // AddUserValidation.CheckValidation(request, _userRepository);

            var user = AddUserFactory.Create(request);

            _userRepository.AddUser(user);
            
            await _userRepository.SaveAsync(cancellationToken);
        }
    }
}