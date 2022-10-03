using Framework.Contracts.Response;
using Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Contracts.Queries.Policy;
using Pumpkin.Domain.Framework.Exceptions;
using Pumpkin.Domain.Framework.Models;
using Pumpkin.Domain.Models.Policy;
using Pumpkin.Domain.Repositories.Policy;

namespace Pumpkin.Infrastructure.Models.Policy;

public class PolicyQueryModel : QueryModelBase, IPolicyQueryModel
{
    private readonly IPolicyQueryRepository _policyQueryRepository;

    public PolicyQueryModel(IHttpContextAccessor accessor, IPolicyQueryRepository policyQueryRepository) : base(accessor)
    {
        _policyQueryRepository = policyQueryRepository;
    }

    public async Task<ListResponse<GetUserPoliciesResultContract>> GetUserPoliciesAsync(GetUserPoliciesQuery query, CancellationToken cancellationToken)
    {
        var policies = await _policyQueryRepository.GetPoliciesAsync(p => p.BuyerId == query.BuyerId, cancellationToken);

        if (policies is null || !policies.Any())
            throw new Dexception(Situation.Make(SitKeys.NotFound));

        var result = policies.Select(policy => new GetUserPoliciesResultContract
        {
            PolicyDraftNo = policy.PolicyNumber,
            SerialNo = policy.SerialNumber,
            FirstName = policy.BuyerFirstName,
            Lastname = policy.BuyerLastname,
            MobileNumber = policy.BuyerMobileNumber,
            NationalCode = policy.BuyerNationalCode,
            Address = policy.BuyerAddress,
            IsActive = policy.IsActive,
            IssuedAt = policy.IssuedAt,
            StartAt = policy.StartAt,
            ExpireAt = policy.ExpireAt,
            PolicyStatus = policy.PolicyStatus
        });

        return ListResponse<GetUserPoliciesResultContract>.Instance(result);
    }
}