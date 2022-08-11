using Microsoft.EntityFrameworkCore.Storage;
using Pumpkin.Domain.Framework.Events;

namespace Pumpkin.Domain.Framework.Services.Requests;

public abstract class CommandBase
{
    public DateTime SentAt { get; set; }
    public TransactionOrders DbOrder { get; set; }
    public IDbContextTransaction Transaction { get; set; }
    public List<DomainEvent> BusEvents { get; set; }

    public CommandBase()
    {
        SentAt = DateTime.UtcNow;
        DbOrder = TransactionOrders.SingleUpdate;
        Transaction = null;
        BusEvents = new();
    }
}