using Pumpkin.Domain.Events.DataTransferObjects.Order;
using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Domain.Events.DomainEvents.Order;

public class NewOrderRegistered : DomainEvent
{
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerMobileNumber { get; set; }
    public string CustomerNationalCode { get; set; }
    public string CustomerAddress { get; set; }
    public Guid CustomerId { get; set; }
    public List<OrderItemsDto> OrderItems { get; set; }

    public NewOrderRegistered()
    {
        // ExchangeName = Globals.Events.StateChangesBus;
        //Routes = new[] {Globals.Events.Routes.Sales};
    }
}