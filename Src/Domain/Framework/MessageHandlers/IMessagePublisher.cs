namespace Domain.Framework.MessageHandlers;

public interface IMessagePublisher
{
    void Publish(BusEvent domainEvent);
}