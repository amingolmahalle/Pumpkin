using Microsoft.EntityFrameworkCore;
using Pumpkin.Domain.Framework.Data.Specifications;

namespace Pumpkin.Infrastructure.Repositories.Order.Queries.Specifications;

public class FindCustomerPoliciesByCustomerIdSpecification : BaseSpecification<Domain.Entities.Order.Order>
{
    public FindCustomerPoliciesByCustomerIdSpecification(Guid customerId) : base(o => o.OrderItems.Any(oi => oi.Policy.CustomerId == customerId))
    {
        AddIncludeWithThenIncludes(q => q
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Policy));

        AddOrderByDescending(o => o.OrderItems.Select(oi => oi.Policy.PolicyNumber));
    }
}