using Microsoft.Extensions.Logging;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Core.Transaction;

public class NullTransactionService : TransactionServiceBase//, IScopedDependency
{
    public NullTransactionService(ILogger<ITransactionService> logger) : base(logger)
    {
    }

    public override ITransactionHandle Begin(TransactionOptions options)
    {
        return new NullTransactionHandle();
    }
}