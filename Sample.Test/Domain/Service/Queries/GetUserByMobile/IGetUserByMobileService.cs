using System.Threading;
using System.Threading.Tasks;

namespace Sample.Test.Domain.Service.Queries.GetUserByMobile
{
    public interface IGetUserByMobileService
    {
        Task<GetUserByMobileResponse> ExecuteAsync(GetUserByMobileRequest request, CancellationToken cancellationToken);
    }
}