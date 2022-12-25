using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Policy;
using Pumpkin.Domain.Framework.Services.Handlers;

namespace Pumpkin.Domain.Application.Commands.Policy;

public interface IPolicyCommands:
    IApplicationCommandHandler<PolicyRegisterCommand, EmptyResponse>
{
}