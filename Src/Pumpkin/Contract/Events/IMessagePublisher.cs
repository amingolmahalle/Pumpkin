namespace Pumpkin.Contract.Events;

public interface IMessagePublisher
{
    void Publish(BusEvent domainEvent);
}