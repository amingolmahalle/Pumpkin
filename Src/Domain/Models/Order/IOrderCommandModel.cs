using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Commands.Order;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Models.Order;

public interface IOrderCommandModel: ICommandModel
{
    Task<EmptyResponse> RegisterOrderAsync (RegisterOrderCommand command, CancellationToken cancellationToken);
    Task<EmptyResponse> SetPaymentStateAsync (PayOrderCommand command, CancellationToken cancellationToken);
    Task<EmptyResponse> ConfirmOrderAsync (ConfirmOrderCommand command, CancellationToken cancellationToken);
}