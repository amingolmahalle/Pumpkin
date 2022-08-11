using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Services.Handlers;

public interface IHandler
{
    public ILog Logger { get; }
}