using System.ComponentModel.DataAnnotations.Schema;
using Framework.Contracts.Response;
using Pumpkin.Domain.Contracts.Inputs.Order;
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
    public List<OrderItemsDto> OrderItems { get; set; } = new();

    public RegisterOrderCommand(RegisterOrderContract request)
    {
        BasketCode = request.BasketCode;
        CustomerFirstName = request.Customer?.FirstName;
        CustomerLastName = request.Customer?.LastName;
        CustomerMobileNumber = request.Customer?.MobileNumber;
        CustomerNationalCode = request.Customer?.NationalCode;
        CustomerAddress = request.Customer?.Address;
        Gender = request.Customer?.Gender;
        foreach (var item in request.Products)
        {
            OrderItems.Add(new OrderItemsDto
            {
                BasketItemCode = item.BasketItemCode,
                ProductCategory = item.Category,
                ProductBrand = item.Brand,
                ProductModel = item.Model,
                ProductPrice = item.Price,
            });
        }
    }
}