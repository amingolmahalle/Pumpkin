using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Repositories;

public interface IRepository
{
    public ILog Logger { get; }
}