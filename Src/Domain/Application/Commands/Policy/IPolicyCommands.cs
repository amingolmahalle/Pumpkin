using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Policy;
using Pumpkin.Domain.Framework.Services.Handlers;

namespace Pumpkin.Domain.Application.Commands.Policy;

public interface IPolicyCommands :
    IApplicationCommandHandler<RegisterPolicyCommand, EmptyResponse>,
    IApplicationCommandHandler<PayPolicyCommand, EmptyResponse>,
    IApplicationCommandHandler<ConfirmPolicyCommand, EmptyResponse>,
    IApplicationCommandHandler<CancelPolicyCommand, EmptyResponse>,
    IApplicationCommandHandler<RefundPolicyCommand, EmptyResponse>
{
}