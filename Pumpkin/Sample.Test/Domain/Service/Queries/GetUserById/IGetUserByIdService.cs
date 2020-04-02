using System.Threading.Tasks;

namespace Sample.Test.Domain.Service.Queries.GetUserById
{
    public interface IGetUserByIdService
    {
        Task<GetUserByIdResponse> ExecuteAsync(GetUserByIdRequest request);
    }
}