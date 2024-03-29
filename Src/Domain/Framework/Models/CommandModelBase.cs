using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Framework.Data.Repositories;
using Pumpkin.Domain.Framework.Entities.Contracts.AggregateRoots;
using Pumpkin.Domain.Framework.Events;
using Pumpkin.Domain.Framework.Extensions;
using Pumpkin.Domain.Framework.Logging;
using Pumpkin.Domain.Framework.Services.Requests;

namespace Pumpkin.Domain.Framework.Models;

public class CommandModelBase : ICommandModel
{
    private readonly ICommandRepositoryBase _repository;
    private readonly IMessagePublisher _publisher;
    public ILog Logger { get; }

    public CommandModelBase(
        ICommandRepositoryBase repository,
        IMessagePublisher publisher,
        IHttpContextAccessor accessor)
    {
        _repository = repository;
        _publisher = publisher;
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
                Payload = @event.Serialize(),
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