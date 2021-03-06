using System.Threading;
using System.Threading.Tasks;

namespace Sample.Test.Domain.Service.Commands.AddUser
{
    public interface IAddUserService
    {
        Task ExecuteAsync(AddUserRequest request, CancellationToken cancellationToken);
    }
}