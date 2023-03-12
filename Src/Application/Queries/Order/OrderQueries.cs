using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Application.Queries.Policy;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Contracts.Queries.Order;
using Pumpkin.Domain.Models.Order;

namespace Pumpkin.Application.Queries.Order;

public class OrderQueries : QueriesBase, IPolicyQueries
{
    private readonly IOrderQueryModel _orderQueryModel;

    public OrderQueries(IHttpContextAccessor accessor, IOrderQueryModel orderQueryModel) : base(accessor)
    {
        _orderQueryModel = orderQueryModel;
    }

    public async Task<ListResponse<GetCustomerPoliciesResultContract>> Handle(GetCustomerPoliciesQuery query, CancellationToken cancellationToken)
        => await _orderQueryModel.GetCustomerPoliciesAsync(query, cancellationToken);
}