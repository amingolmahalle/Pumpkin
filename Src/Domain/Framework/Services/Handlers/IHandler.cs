using Domain.Framework.Logging;

namespace Domain.Framework.Services.Handlers;

public interface IHandler
{
    public ILog Logger { get; }
}