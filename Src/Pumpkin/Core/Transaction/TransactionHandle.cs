using Microsoft.EntityFrameworkCore.Storage;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Core.Transaction;

public class TransactionHandle : ITransactionHandle
{
    public TransactionHandle(IDbContextTransaction transaction)
    {
        Transaction = transaction;
    }

    private IDbContextTransaction Transaction { get; }

    public void Complete()
    {
        Transaction.Commit();
    }

    public void Dispose()
    {
        Transaction.Dispose();
    }

    public void Rollback()
    {
        Transaction.Rollback();
    }
}

public class NullTransactionHandle : ITransactionHandle
{
    public void Complete()
    {
    }

    public void Dispose()
    {
    }

    public void Rollback()
    {
    }
}