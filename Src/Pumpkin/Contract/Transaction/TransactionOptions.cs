using System.Transactions;

namespace Pumpkin.Contract.Transaction;

public class TransactionOptions
{
    public bool IsEnabled { get; set; } = true;
        
    public TimeSpan? Timeout { get; set; }
        
    public IsolationLevel? IsolationLevel { get; set; }
}