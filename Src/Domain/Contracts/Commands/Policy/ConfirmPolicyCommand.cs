using Framework.Contracts.Response;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Policy;

public class ConfirmPolicyCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
}