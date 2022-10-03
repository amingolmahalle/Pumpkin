using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Contracts.Queries.Policy;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Models.Policy;

public interface IPolicyQueryModel : IQueryModel
{
    Task<ListResponse<GetUserPoliciesResultContract>> GetUserPoliciesAsync(GetUserPoliciesQuery query, CancellationToken cancellationToken);
}