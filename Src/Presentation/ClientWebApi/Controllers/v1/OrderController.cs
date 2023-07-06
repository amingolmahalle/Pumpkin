using Framework.Contracts.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Domain.Contracts.Commands.Order;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Contracts.Queries.Order;

namespace ClientWebApi.Controllers.v1;

[ApiVersion("1.0")]
[Area("digital-shopping")]
public class OrderController : BaseController
{
    private readonly IMediator _mediator;

    public OrderController(IHttpContextAccessor accessor, IMediator mediator) : base(accessor)
    {
        _mediator = mediator;
    }

    [HttpGet("get_customer_policies")]
    public async Task<ActionResult<ListResponse<GetCustomerPoliciesResultContract>>> GetCustomerPolicies(GetCustomerPoliciesContract request, CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new GetCustomerPoliciesQuery(request), cancellationToken));

    [HttpPost("register-order")]
    public async Task<ActionResult<RegisterOrderContract>> RegisterOrder([FromBody] RegisterOrderContract request, CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new RegisterOrderCommand(request), cancellationToken));

    [HttpPut("pay-order")]
    public async Task<ActionResult<PayOrderContract>> PayOrder(PayOrderContract request, CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new PayOrderCommand(request), cancellationToken));

    [HttpPut("confirm-order")]
    public async Task<ActionResult<ConfirmOrderContract>> ConfirmOrder(ConfirmOrderContract request, CancellationToken cancellationToken)
        => Dynamic(await _mediator.Send(new ConfirmOrderCommand(request), cancellationToken));

    // [HttpPut("cancel-order")]
    // public async Task<ActionResult<CancelOrderContract>> CancelOrder(CancelOrderContract request, CancellationToken cancellationToken)
    //     => Dynamic(await _mediator.Send(new CancelOrderCommand(request), cancellationToken));
}