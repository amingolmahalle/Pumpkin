using System.Runtime.ExceptionServices;

namespace Pumpkin.Contract.Transaction;

public interface ITransactionService
{
    ITransactionHandle Begin(TransactionOptions options);

    Task ExecuteAsync(
        Func<Task> action,
        Func<ExceptionDispatchInfo, Task> onFailure = null,
        TransactionOptions options = null);

    void Execute(
        Action action,
        Action<ExceptionDispatchInfo> onFailure = null,
        TransactionOptions options = null);
}