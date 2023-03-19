using Framework.Contracts.Response;
using Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Contracts.Commands.Order;
using Pumpkin.Domain.Events.DomainEvents.Order;
using Pumpkin.Domain.Framework.Data.Repositories;
using Pumpkin.Domain.Framework.Events;
using Pumpkin.Domain.Framework.Exceptions;
using Pumpkin.Domain.Framework.Models;
using Pumpkin.Domain.Framework.Serialization;
using Pumpkin.Domain.Models.Order;
using Pumpkin.Domain.Repositories.Order;

namespace Pumpkin.Infrastructure.Models.Order;

public class OrderCommandModel : CommandModelBase, IOrderCommandModel
{
    private readonly IOrderCommandRepository _orderCommandRepository;

    public OrderCommandModel(ICommandRepositoryBase repository, IMessagePublisher publisher, IHttpContextAccessor accessor, ISerializer serializer,
        IOrderCommandRepository orderCommandRepository) : base(repository, publisher, accessor, serializer)
    {
        _orderCommandRepository = orderCommandRepository;
    }

    public async Task<EmptyResponse> RegisterOrderAsync(RegisterOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order.Order().Create(
            command.BasketCode,
            command.CustomerFirstName,
            command.CustomerLastName,
            command.CustomerMobileNumber,
            command.CustomerNationalCode,
            command.CustomerAddress,
            command.CustomerId.GetValueOrDefault(),
            command.TotalProductPrice);

        order.Apply(new NewOrderRegistered
        {
            CustomerId = command.CustomerId.GetValueOrDefault(),
            CustomerFirstName = command.CustomerFirstName,
            CustomerLastName = command.CustomerLastName,
            CustomerMobileNumber = command.CustomerMobileNumber,
            CustomerNationalCode = command.CustomerNationalCode,
            CustomerAddress = command.CustomerAddress,
            OrderItems = command.OrderItems,
            Payload = new Dictionary<string, object>
            {
                { "BasketCode", command.BasketCode },
            }
        });

        await _orderCommandRepository.AddAsync(order, cancellationToken);
        await HandleTransactionAsync(command, order, cancellationToken);

        return EmptyResponse.Instance();
    }

    public async Task<EmptyResponse> SetPaymentStateAsync(PayOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderCommandRepository.FindOrderByBasketIdAsync(command.BasketCode, cancellationToken);

        if (order is null)
            throw new Dexception(Situation.Make("Order not found.")); 

        order.Apply(new OrderPayed
        {
            TrackingCode = command.TrackingCode,
            IsPaid = command.IsPaid,
        });

        _orderCommandRepository.Modify(order);
        await HandleTransactionAsync(command, order, cancellationToken);

        return EmptyResponse.Instance();
    }

    public async Task<EmptyResponse> ConfirmOrderAsync(ConfirmOrderCommand command, CancellationToken cancellationToken)
    {
        bool hasExistSerialNumber = await _orderCommandRepository.HasOrderByDeviceSerialNumberAsync(command.DeviceSerialNumber, cancellationToken);

        if (hasExistSerialNumber)
            throw new Dexception(Situation.Make("Device serial number is Duplicate."));

        var order = await _orderCommandRepository.FindOrderByBasketIdAsync(command.BasketCode, cancellationToken);

        if (order is null)
            throw new Dexception(Situation.Make("Order not found.")); //SitKeys.SchCartNotFound

        order.Apply(new OrderConfirmed
        {
            BasketItemCode = command.BasketItemCode,
            DeviceSerialNumber = command.DeviceSerialNumber,
        });

        _orderCommandRepository.Modify(order);
        await HandleTransactionAsync(command, order, cancellationToken);

        return EmptyResponse.Instance();
    }
}