namespace Pumpkin.Domain.Framework.Services.Requests;

public enum TransactionOrders
{
    SingleUpdate = 0,
    SingleTransaction = 5,
    StartTransaction = 10,
    ParticipateInTransaction = 20,
    CommitTransaction = 100,
    RollbackTransaction = 200
}