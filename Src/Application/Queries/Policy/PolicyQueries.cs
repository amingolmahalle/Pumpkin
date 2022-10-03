using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Application.Queries.Policy;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Contracts.Queries.Policy;
using Pumpkin.Domain.Models.Policy;

namespace Pumpkin.Application.Queries.Policy;

public class PolicyQueries : QueriesBase, IPolicyQueries
{
    private readonly IPolicyQueryModel _policyQueryModel;

    public PolicyQueries(IHttpContextAccessor accessor, IPolicyQueryModel policyQueryModel) : base(accessor)
    {
        _policyQueryModel = policyQueryModel;
    }

    public async Task<ListResponse<GetUserPoliciesResultContract>> Handle(GetUserPoliciesQuery query, CancellationToken cancellationToken)
        => await _policyQueryModel.GetUserPoliciesAsync(query, cancellationToken);
}