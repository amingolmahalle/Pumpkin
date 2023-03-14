using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Order;
using Pumpkin.Domain.Framework.Services.Handlers;

namespace Pumpkin.Domain.Application.Commands.Policy;

public interface IOrderCommands :
    IApplicationCommandHandler<RegisterOrderCommand, EmptyResponse>,
    IApplicationCommandHandler<PayOrderCommand, EmptyResponse>,
    IApplicationCommandHandler<ConfirmOrderCommand, EmptyResponse>,
    IApplicationCommandHandler<CancelOrderCommand, EmptyResponse>
{
}