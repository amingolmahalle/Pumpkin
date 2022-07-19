using Domain.Framework.MessageHandlers;

namespace Domain.Framework.Entities.Contracts.AggregateRoots;

public abstract class AggregateRoot : IEntity
{
}

public abstract class AggregateRoot<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    
    private List<DomainEvent> _events;

    protected AggregateRoot() => _events = new();

    protected void AddEvent(DomainEvent @event) => _events.Add(@event);

    public IEnumerable<DomainEvent> GetChanges() => _events;

    public void CLearChanges() => _events = new();

    public void Apply(DomainEvent @event)
    {
        EnsureReadyState(@event);
        When(@event);
        EnsureValidState();
        _events.Add(@event);
    }

    protected abstract void EnsureReadyState(object @event);

    protected abstract void When(object @event);

    protected abstract void EnsureValidState();
}