namespace Pumpkin.Domain.Framework.Data.Repositories;

public interface IUnitOfWork : IDisposable
{
    void Commit();

    void Rollback();
}