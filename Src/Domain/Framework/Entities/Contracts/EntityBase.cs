
namespace Domain.Framework.Entities.Contracts;

public abstract class EntityBase : IEntity
{
    public void Apply(object @event)
    {
        EnsureReadyState(@event);
        When(@event);
        EnsureValidState();
    }

    protected abstract void EnsureReadyState(object @event);

    protected abstract void When(object @event);

    protected abstract void EnsureValidState();
}

public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }

    public void Apply(object @event)
    {
        EnsureReadyState(@event);
        When(@event);
        EnsureValidState();
    }

    protected abstract void EnsureReadyState(object @event);

    protected abstract void When(object @event);

    protected abstract void EnsureValidState();
}