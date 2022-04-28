using System.Threading;
using System.Threading.Tasks;

namespace SampleWebApi.Domain.Service.Queries.GetUserById
{
    public interface IGetUserByIdService
    {
        Task<GetUserByIdResponse> ExecuteAsync(GetUserByIdRequest request, CancellationToken cancellationToken);
    }
}