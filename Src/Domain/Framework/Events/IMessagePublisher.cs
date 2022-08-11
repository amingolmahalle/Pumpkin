namespace Pumpkin.Domain.Framework.Events;

public interface IMessagePublisher
{
    void Publish(BusEvent domainEvent);
}