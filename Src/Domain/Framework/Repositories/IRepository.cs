
using Domain.Framework.Logging;

namespace Framework.Repositories;

public interface IRepository
{
    public ILog Logger { get; }
}