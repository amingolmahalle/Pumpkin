using Framework.Contracts.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Domain.Contracts.Inputs.Policy;
using Pumpkin.Domain.Contracts.Queries.Policy;

namespace ClientWebApi.Controllers.v1;

[ApiVersion("1.0")]
[Area("shopping")]
public class PolicyController : BaseController
{
    private readonly IMediator _mediator;

    public PolicyController(IHttpContextAccessor accessor, IMediator mediator) : base(accessor)
    {
        _mediator = mediator;
    }

    [HttpGet("get_user_policies")]
    public async Task<ActionResult<ListResponse<GetUserPoliciesResultContract>>> GetUserPolicies(GetUserPoliciesContract request, CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new GetUserPoliciesQuery(request), cancellationToken));
}