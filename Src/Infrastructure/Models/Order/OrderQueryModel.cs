using Framework.Contracts.Response;
using Framework.Exceptions;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Contracts.Queries.Order;
using Pumpkin.Domain.Framework.Exceptions;
using Pumpkin.Domain.Framework.Models;
using Pumpkin.Domain.Models.Order;
using Pumpkin.Domain.Repositories.Order;

namespace Pumpkin.Infrastructure.Models.Order;

public class OrderQueryModel : QueryModelBase, IOrderQueryModel
{
    private readonly IOrderQueryRepository _orderQueryRepository;

    public OrderQueryModel(
        IHttpContextAccessor accessor,
        IOrderQueryRepository orderQueryRepository) : base(accessor)
    {
        _orderQueryRepository = orderQueryRepository;
    }

    public async Task<ListResponse<GetCustomerPoliciesResultContract>> GetCustomerPoliciesAsync(GetCustomerPoliciesQuery query, CancellationToken cancellationToken)
    {
        var policies = await _orderQueryRepository.FindPoliciesAsync(query.CustomerId, cancellationToken);

        if (policies is null || !policies.Any())
            throw new Dexception(Situation.Make(SitKeys.NotFound));

        var result = policies.Select(policy => new GetCustomerPoliciesResultContract
        {
            PolicyDraftNo = policy.PolicyNumber,
            SerialNo = policy.OrderItem?.DeviceSerialNumber,
            FirstName = policy.CustomerFirstName,
            LastName = policy.CustomerLastName,
            MobileNumber = policy.CustomerMobileNumber,
            NationalCode = policy.CustomerNationalCode,
            Address = policy.CustomerAddress,
            IsActive = policy.IsActive,
            IssuedAt = policy.IssuedAt,
            StartAt = policy.StartAt,
            ExpireAt = policy.ExpireAt,
        });

        return ListResponse<GetCustomerPoliciesResultContract>.Instance(result);
    }
}