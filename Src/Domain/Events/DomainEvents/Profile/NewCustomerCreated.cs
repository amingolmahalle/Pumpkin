using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Domain.Events.DomainEvents.Profile;

public class NewCustomerCreated:DomainEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public string NationalCode { get; set; }
    public string Address { get; set; }
    public bool? Gender { get; set; }
    
    public NewCustomerCreated()
    {
        ExchangeName = Events.StateChangesBus;
        Routes = new[] {Events.Routes.Sales};
    }
}