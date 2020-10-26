using System.Threading;
using System.Threading.Tasks;
using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Commands.EditUser;

namespace Sample.Test.Service.Commands.EditUser
{
    public class EditUserService : IEditUserService
    {
        private readonly IUserRepository _userRepository;

        public EditUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(EditUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

            EditUserFactory.MapEntity(ref user, request);

            await _userRepository.EditUserAsync(user, cancellationToken);
        }
    }
}