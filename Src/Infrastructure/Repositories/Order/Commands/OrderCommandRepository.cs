using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Repositories.Order;
using Pumpkin.Infrastructure.Framework.Data.Context;
using Pumpkin.Infrastructure.Framework.Data.Repositories;
using Pumpkin.Infrastructure.Repositories.Order.Commands.Specifications;

namespace Pumpkin.Infrastructure.Repositories.Order.Commands;

public class OrderCommandRepository : CommandRepository<Domain.Entities.Order.Order, Guid>, IOrderCommandRepository
{
    public OrderCommandRepository(DbContextBase context) : base(context)
    {
    }

    public async Task<bool> HasOrderByDeviceSerialNumberAsync(string deviceUniqueNumber, CancellationToken cancellationToken = default)
        => await ApplySpecification(new FindOrderItemByDeviceSerialNumberSpecification(deviceUniqueNumber))
            .AnyAsync(o => o.OrderItems.Any(), cancellationToken);

    public async Task<Domain.Entities.Order.Order> FindOrderByBasketIdAsync(string basketId, CancellationToken cancellationToken)
        => await ApplySpecification(new FindOrderItemByDeviceSerialNumberSpecification(basketId))
            .FirstOrDefaultAsync(cancellationToken);
}