using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Domain.Contracts.Queries.Policy;

public class GetUserPoliciesQuery: IApplicationQuery<ListResponse<GetUserPoliciesResultContract>>
{
    public Guid BuyerId { get; set; }

    public GetUserPoliciesQuery(GetUserPoliciesContract request)
    {
        BuyerId = request.BuyerId;
    }
}