using System.Threading;
using System.Threading.Tasks;

namespace Sample.Test.Domain.Service.Commands.EditUser
{
    public interface IEditUserService
    {
        Task ExecuteAsync(EditUserRequest request, CancellationToken cancellationToken);
    }
}