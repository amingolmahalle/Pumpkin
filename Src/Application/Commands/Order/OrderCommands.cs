using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Application.Commands.Policy;
using Pumpkin.Domain.Contracts.Commands.Order;
using Pumpkin.Domain.Contracts.Commands.Profile;
using Pumpkin.Domain.Models.Order;
using Pumpkin.Domain.Models.Profile;

namespace Pumpkin.Application.Commands.Order;

public class OrderCommands : CommandsBase, IOrderCommands
{
    private readonly IOrderCommandModel _orderCommandModel;
    private readonly IUserCommandModel _userCommandModel;

    public OrderCommands(IHttpContextAccessor accessor, IOrderCommandModel orderCommandModel, IUserCommandModel userCommandModel) : base(accessor)
    {
        _orderCommandModel = orderCommandModel;
        _userCommandModel = userCommandModel;
    }

    public async Task<EmptyResponse> Handle(RegisterOrderCommand command, CancellationToken cancellationToken)
    {
        // TODO: Transaction here on inside Model ...
        var createOrGrabCustomerCommand = new CreateOrGrabCustomerCommand
        {
            FirstName = command.CustomerFirstName,
            LastName = command.CustomerLastName,
            MobileNumber = command.CustomerMobileNumber,
            NationalCode = command.CustomerNationalCode,
            Address = command.CustomerAddress,
            Gender = command.Gender,
        };
        Guid customerId = (await _userCommandModel.CreateOrGrabCustomerAsync(createOrGrabCustomerCommand, cancellationToken)).Data;

        command.CustomerId = customerId;

        return await _orderCommandModel.RegisterOrderAsync(command, cancellationToken);
    }

    public async Task<EmptyResponse> Handle(PayOrderCommand command, CancellationToken cancellationToken)
        => await _orderCommandModel.SetPaymentStateAsync(command, cancellationToken);

    public async Task<EmptyResponse> Handle(ConfirmOrderCommand command, CancellationToken cancellationToken)
        => await _orderCommandModel.ConfirmOrderAsync(command, cancellationToken);

    public async Task<EmptyResponse> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}