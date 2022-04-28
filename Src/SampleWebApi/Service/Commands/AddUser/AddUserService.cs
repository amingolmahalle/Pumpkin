using SampleWebApi.Domain.Service.Commands.AddUser;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Commands.AddUser;

public class AddUserService : IAddUserService
{
    private readonly IUserRepository _userRepository;

    public AddUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(AddUserRequest request, CancellationToken cancellationToken)
    {
        var user = AddUserFactory.MapEntity(request);

        await _userRepository.AddUserAsync(user, cancellationToken);
    }
}