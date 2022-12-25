using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Policy;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Models.Policy;

public interface IPolicyCommandModel: ICommandModel
{
    Task<EmptyResponse> PolicyRegisterAsync (RegisterPolicyCommand command, CancellationToken cancellationToken);
}