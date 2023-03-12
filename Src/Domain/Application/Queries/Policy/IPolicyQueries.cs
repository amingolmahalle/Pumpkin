using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Contracts.Queries.Order;
using Pumpkin.Domain.Framework.Services.Handlers;

namespace Pumpkin.Domain.Application.Queries.Policy;

public interface IPolicyQueries:
    IApplicationQueryHandler<GetCustomerPoliciesQuery, ListResponse<GetCustomerPoliciesResultContract>>
{
}