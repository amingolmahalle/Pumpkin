using Pumpkin.Domain.Framework.Data.Repositories;

namespace Pumpkin.Domain.Repositories.Order;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order.Order, Guid>
{
    Task<Entities.Order.Order> FindOrderByBasketIdAsync(string basketId, CancellationToken cancellationToken = default);
    Task<bool> HasOrderByDeviceSerialNumberAsync(string deviceSerialNumber, CancellationToken cancellationToken = default);
}