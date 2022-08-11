using Domain.Framework.Entities.Contracts.AggregateRoots;
using Domain.Framework.Events;
using Domain.Framework.Logging;
using Domain.Framework.Repositories;
using Domain.Framework.Serialization;
using Domain.Framework.Services.Requests;
using Microsoft.AspNetCore.Http;

namespace Domain.Framework.Models;

public class CommandModelBase : ICommandModel
{
    private readonly ICommandRepositoryBase _repository;
    private readonly IMessagePublisher _publisher;
    private readonly ISerializer _serializer;
    public ILog Logger { get; }

    public CommandModelBase(
        ICommandRepositoryBase repository,
        IMessagePublisher publisher,
        IHttpContextAccessor accessor,
        ISerializer serializer)
    {
        _repository = repository;
        _publisher = publisher;
        _serializer = serializer;
        Logger = LogManager.GetLogger<CommandModelBase>();
    }

    protected void PublishToBus(IEnumerable<DomainEvent> events)
    {
        foreach (var @event in events)
            PublishToBus(new BusEvent
            {
                ExchangeName = @event.ExchangeName,
                Routes = @event.Routes,
                EventType = @event.GetType().ToString(),
                Payload = _serializer.Serialize(@event),
                Retry = 1
            });
    }

    protected void PublishToBus(BusEvent @event)
        => _publisher.Publish(@event);

    protected async Task HandleTransactionAsync(CommandBase command, GuidAuditableAggregateRoot aggregateRoot,
        CancellationToken cancellationToken)
        => await HandleTransactionAsync(command, new[] { aggregateRoot }, cancellationToken);

    protected async Task HandleTransactionAsync(CommandBase command, IEnumerable<GuidAuditableAggregateRoot> aggregateRoots,
        CancellationToken cancellationToken)
    {
        switch (command.DbOrder)
        {
            case TransactionOrders.SingleUpdate:
                await _repository.SaveChangesAsync(cancellationToken);
                PublishToBus(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.SingleTransaction:
                command.Transaction = await _repository.StartTransAsync(cancellationToken: cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
                await command.Transaction.CommitAsync(cancellationToken);

                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                PublishToBus(command.BusEvents);
                break;

            case TransactionOrders.StartTransaction:
                command.Transaction = await _repository.StartTransAsync(cancellationToken: cancellationToken);

                await _repository.SaveChangesAsync(cancellationToken);
                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.ParticipateInTransaction:
                await _repository.SaveChangesAsync(cancellationToken);
                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                break;

            case TransactionOrders.CommitTransaction:
                await _repository.SaveChangesAsync(cancellationToken);
                await command.Transaction.CommitAsync(cancellationToken);

                command.BusEvents.AddRange(aggregateRoots.SelectMany(x => x.GetChanges()));
                PublishToBus(command.BusEvents);
                break;

            case TransactionOrders.RollbackTransaction:
                await command.Transaction.RollbackAsync(cancellationToken);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}