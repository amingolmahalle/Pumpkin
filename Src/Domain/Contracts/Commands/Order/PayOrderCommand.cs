using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Order;

public class PayOrderCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string BasketCode { get; set; }
    public string PaymentTrackingCode { get; set; }
    public bool IsPaid { get; set; }

    public PayOrderCommand(PayOrderContract request)
    {
        BasketCode = request.BasketCode;
        PaymentTrackingCode = request.PaymentTrackingCode;
        IsPaid = request.IsPaid;
    }
}