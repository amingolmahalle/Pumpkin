using System.ComponentModel.DataAnnotations.Schema;
using Framework.Contracts.Response;
using Pumpkin.Domain.Events.DataTransferObjects.Order;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Contracts.Commands.Order;

public class RegisterOrderCommand : CommandBase, IApplicationCommand<EmptyResponse>
{
    public string BasketCode { get; set; }
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobileNumber { get; set; }
    public string CustomerNationalCode { get; set; }
    public string CustomerAddress { get; set; }
    public bool? Gender { get; set; }
    [NotMapped] public Guid? CustomerId { get; set; }
    public decimal TotalProductPrice { get; set; }
    public List<OrderItemsDto> OrderItems { get; set; }
}