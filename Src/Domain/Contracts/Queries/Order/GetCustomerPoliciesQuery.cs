using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Domain.Contracts.Queries.Order;

public class GetCustomerPoliciesQuery: IApplicationQuery<ListResponse<GetCustomerPoliciesResultContract>>
{
    public Guid CustomerId { get; set; }

    public GetCustomerPoliciesQuery(GetCustomerPoliciesContract request)
    {
        CustomerId = request.CustomerId;
    }
}