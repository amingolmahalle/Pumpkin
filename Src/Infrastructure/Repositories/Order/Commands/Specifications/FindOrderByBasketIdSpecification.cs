using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Data.Specifications;

namespace Pumpkin.Infrastructure.Repositories.Order.Commands.Specifications;

public class FindOrderByBasketIdSpecification : BaseSpecification<Domain.Entities.Order.Order>
{
    public FindOrderByBasketIdSpecification(string basketId) : base(o => o.OrderItems.Any(oi => oi.DeviceSerialNumber == basketId))
    {
        AddIncludeWithThenIncludes(q => q
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Policy));
    }
}