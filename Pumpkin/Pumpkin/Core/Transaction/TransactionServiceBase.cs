using System;
using System.Data;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pumpkin.Contract.Transaction;

namespace Pumpkin.Core.Transaction
{
    public abstract class TransactionServiceBase : ITransactionService
    {
        private readonly ILogger<ITransactionService> _logger;

        public TransactionServiceBase(ILogger<ITransactionService> logger)
        {
            _logger = logger;
        }

        public abstract ITransactionHandle Begin(TransactionOptions options);

        public void Execute(
            Action action,
            Action<ExceptionDispatchInfo> onFailure = null,
            TransactionOptions options = null)
        {
            options ??= new TransactionOptions();

            using var tran = Begin(options);
            try
            {
                action();
                tran.Complete();
            }
            catch (Exception ex)
            {
                tran.Rollback();

                if (onFailure == null)
                    throw;

                onFailure(ExceptionDispatchInfo.Capture(ex));
            }
        }

        public async Task ExecuteAsync(
            Func<Task> action,
            Func<ExceptionDispatchInfo, Task> onFailure = null,
            TransactionOptions options = null)
        {
            options ??= new TransactionOptions();

            using var tran = Begin(options);
            try
            {
                await action();
                tran.Complete();
            }

            catch (Exception ex)
            {
                tran.Rollback();

                if (onFailure == null)
                    throw;

                await onFailure(ExceptionDispatchInfo.Capture(ex));
            }
        }

        public IsolationLevel ToSystemDataIsolationLevel(System.Transactions.IsolationLevel isolationLevel)
        {
            switch (isolationLevel)
            {
                case System.Transactions.IsolationLevel.Chaos:
                    return IsolationLevel.Chaos;
                case System.Transactions.IsolationLevel.ReadCommitted:
                    return IsolationLevel.ReadCommitted;
                case System.Transactions.IsolationLevel.ReadUncommitted:
                    return IsolationLevel.ReadUncommitted;
                case System.Transactions.IsolationLevel.RepeatableRead:
                    return IsolationLevel.RepeatableRead;
                case System.Transactions.IsolationLevel.Serializable:
                    return IsolationLevel.Serializable;
                case System.Transactions.IsolationLevel.Snapshot:
                    return IsolationLevel.Snapshot;
                case System.Transactions.IsolationLevel.Unspecified:
                    return IsolationLevel.Unspecified;
                default:
                    throw new Exception("Unknown isolation level: " + isolationLevel);
            }
        }
    }
}