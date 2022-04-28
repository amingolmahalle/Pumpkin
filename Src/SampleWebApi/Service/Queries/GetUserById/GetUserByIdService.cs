using SampleWebApi.Domain.Service.Queries.GetUserById;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Queries.GetUserById;

public class GetUserByIdService : IGetUserByIdService
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResponse> ExecuteAsync(GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.Id, cancellationToken);

        if (user == null)
            return null;

        var result = GetUserByIdFactory.MapResponse(user);

        return result;
    }
}