using System.Threading;
using System.Threading.Tasks;
using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Queries.GetUserByMobile;

namespace Sample.Test.Service.Queries.GetUserByMobile
{
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
}