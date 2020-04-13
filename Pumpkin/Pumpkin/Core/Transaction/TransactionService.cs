using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pumpkin.Contract.Transaction;
using Pumpkin.Data;

namespace Pumpkin.Core.Transaction
{
    public class TransactionService<TDbContext> : TransactionServiceBase
        where TDbContext : DatabaseContext
    {
        public TransactionService(TDbContext activeDbContext, ILogger<ITransactionService> logger) : base(logger)
        {
            ActiveDbContext = activeDbContext;
        }

        private TDbContext ActiveDbContext { get; set; }

        public override ITransactionHandle Begin(TransactionOptions options)
        {
            if (!options.IsEnabled)
                return new NullTransactionHandle();

            if (options.Timeout.HasValue && !ActiveDbContext.Database.GetCommandTimeout().HasValue)
            {
                ActiveDbContext.Database.SetCommandTimeout((int) options.Timeout.Value.TotalSeconds);
            }

            return new TransactionHandle(
                ActiveDbContext.Database.BeginTransaction(
                    ToSystemDataIsolationLevel(options.IsolationLevel ??
                                               System.Transactions.IsolationLevel.ReadCommitted)));
        }
    }
}