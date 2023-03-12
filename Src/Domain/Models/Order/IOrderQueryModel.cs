using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Contracts.Queries.Order;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Models.Order;

public interface IOrderQueryModel : IQueryModel
{
    Task<ListResponse<GetCustomerPoliciesResultContract>> GetCustomerPoliciesAsync(GetCustomerPoliciesQuery query, CancellationToken cancellationToken);
}