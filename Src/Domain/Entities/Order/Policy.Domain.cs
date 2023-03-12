using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Events.DomainEvents.Order;

namespace Pumpkin.Domain.Entities.Order;

public partial class Policy
{
    protected override void EnsureReadyState(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void When(object @event)
    {
        switch (@event)
        {
            case OrderConfirmed:
                var currentDate = DateTime.Now;
                CurrentState = PolicyStates.Activated;
                IsActive = true;
                IssuedAt = currentDate;
                StartAt = currentDate.AddHours(1);
                ExpireAt = currentDate.AddYears(1);
                break;
        }
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}