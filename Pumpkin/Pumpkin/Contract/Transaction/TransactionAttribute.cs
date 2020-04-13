using System;
using System.Transactions;

namespace Pumpkin.Contract.Transaction
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Interface)]
    public class TransactionAttribute : Attribute
    {
        public TransactionAttribute()
        {
        }
        
        public TransactionAttribute(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public TransactionAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public TransactionAttribute(bool isEnabled, int timeout)
        {
            IsEnabled = isEnabled;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        public TransactionAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }

        public TransactionAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }
        
        public TimeSpan? Timeout { get; set; }

        public IsolationLevel? IsolationLevel { get; set; }

        public bool IsEnabled { get; set; } = true;
        
        public TransactionOptions CreateOptions()
        {
            return new TransactionOptions
            {
                IsEnabled = IsEnabled,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout
            };
        }
    }
}
