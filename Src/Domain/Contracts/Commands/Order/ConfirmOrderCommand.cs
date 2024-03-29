using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Order;

public class ConfirmOrderCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string BasketCode { get; set; }
    public string BasketItemCode { get; set; }
    public string DeviceSerialNumber { get; set; }

    public ConfirmOrderCommand(ConfirmOrderContract request)
    {
        BasketCode = request.BasketCode;
        BasketItemCode = request.BasketItemCode;
        DeviceSerialNumber = request.DeviceSerialNumber;
    }
}