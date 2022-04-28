namespace Pumpkin.Contract.Transaction;

public interface ITransactionHandle : IDisposable
{
    void Complete();
        
    void Rollback();

}