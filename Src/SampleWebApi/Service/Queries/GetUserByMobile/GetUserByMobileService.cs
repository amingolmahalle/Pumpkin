using SampleWebApi.Domain.Service.Queries.GetUserByMobile;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Service.Queries.GetUserByMobile;

public class GetUserByMobileService : IGetUserByMobileService
{
    private readonly IUserRepository _userRepository;

    public GetUserByMobileService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByMobileResponse> ExecuteAsync(GetUserByMobileRequest request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByMobileAsync(request.MobileNumber, cancellationToken);

        if (user == null)
            return null;

        var result = GetUserByMobileFactory.MapResponse(user);

        return result;
    }
}