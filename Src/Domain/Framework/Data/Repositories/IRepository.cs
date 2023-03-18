using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Data.Repositories;

public interface IRepository
{
    public ILog Logger { get; }
}