using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Application.Commands.Policy;
using Pumpkin.Domain.Contracts.Commands.Policy;
using Pumpkin.Domain.Models.Policy;

namespace Pumpkin.Application.Commands.Policy;

public class PolicyCommands : CommandsBase, IPolicyCommands
{
    private readonly IPolicyCommandModel _policyCommandModel;
    public PolicyCommands(IHttpContextAccessor accessor, IPolicyCommandModel policyCommandModel) : base(accessor)
    {
        _policyCommandModel = policyCommandModel;
    }

    public async Task<EmptyResponse> Handle(PolicyRegisterCommand command, CancellationToken cancellationToken)
        => await _policyCommandModel.PolicyRegisterAsync(command, cancellationToken);
}