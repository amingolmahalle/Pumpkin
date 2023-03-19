using Pumpkin.Domain.Framework.Data.Specifications;

namespace Pumpkin.Infrastructure.Repositories.Order.Commands.Specifications;

public class FindOrderItemByDeviceSerialNumberSpecification : BaseSpecification<Domain.Entities.Order.Order>
{
    public FindOrderItemByDeviceSerialNumberSpecification(string deviceSerialNumber) : base(o => o.OrderItems.Any(oi => oi.DeviceSerialNumber == deviceSerialNumber))
    {
        AddInclude(o => o.OrderItems);
    }
}