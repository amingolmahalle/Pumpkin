namespace Pumpkin.Domain.Entities.Policy;

public partial class Order
{
    protected override void EnsureReadyState(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void When(object @event)
    {
        throw new NotImplementedException();
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}