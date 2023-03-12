using Framework.Contracts.Response;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Order;

public class PayOrderCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string BasketCode { get; set; }
    public string TrackingCode { get; set; }
    public bool IsPaid { get; set; }
}