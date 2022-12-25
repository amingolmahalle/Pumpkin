using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Application.Commands;

public abstract class CommandsBase
{
    public ILog Logger { get; }

    public CommandsBase(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<CommandsBase>();
    }
}