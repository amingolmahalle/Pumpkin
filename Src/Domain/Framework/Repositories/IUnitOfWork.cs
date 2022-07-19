namespace ALBB_SpaceTravel.Domain.Framework.Repositories;

public interface IUnitOfWork : IDisposable
{
    void Commit();

    void Rollback();
}