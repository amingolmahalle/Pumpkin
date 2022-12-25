using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Contracts.Queries.Policy;
using Pumpkin.Domain.Framework.Services.Handlers;

namespace Pumpkin.Domain.Application.Queries.Policy;

public interface IPolicyQueries:
    IApplicationQueryHandler<GetUserPoliciesQuery, ListResponse<GetUserPoliciesResultContract>>
{
}