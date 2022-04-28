using System.Threading;
using System.Threading.Tasks;

namespace SampleWebApi.Domain.Service.Queries.GetUserByMobile
{
    public interface IGetUserByMobileService
    {
        Task<GetUserByMobileResponse> ExecuteAsync(GetUserByMobileRequest request, CancellationToken cancellationToken);
    }
}