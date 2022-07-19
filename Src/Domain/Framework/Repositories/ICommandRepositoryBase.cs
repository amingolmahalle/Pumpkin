using System.Data;
using Framework.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Framework.Repositories;

public interface ICommandRepositoryBase : IRepository
{
    Task<IDbContextTransaction> StartTransAsync(IsolationLevel level = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);

    void SaveChange();
}