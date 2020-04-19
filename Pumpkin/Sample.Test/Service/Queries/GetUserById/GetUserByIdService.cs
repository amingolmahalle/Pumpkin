using System.Threading;
using System.Threading.Tasks;
using Sample.Test.Domain.Entity.UserAggregate;
using Sample.Test.Domain.Service.Queries.GetUserById;

namespace Sample.Test.Service.Queries.GetUserById
{
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

            var result = GetUserByIdFactory.Create(user);

            return result;
        }
    }
}