using System.Threading.Tasks;
using Sample.Test.Data.Repositories;
using Sample.Test.Domain.Service.Queries.GetUserById;

namespace Sample.Test.Service.Queries.GetUserById
{
    public class GetUserByIdService : IGetUserByIdService
    {
        private IUserRepository UserRepository { get; }
        public GetUserByIdService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<GetUserByIdResponse> ExecuteAsync(GetUserByIdRequest request)
        {
            var user = await UserRepository.GetByIdAsync(request.Id);

            if (user == null)
                return null;

            var result = GetUserByIdFactory.Create(user);

            return result;
        }
    }
}