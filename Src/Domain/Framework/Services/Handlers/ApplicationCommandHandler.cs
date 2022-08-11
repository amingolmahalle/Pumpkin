using MediatR;
using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Framework.Logging;
using Pumpkin.Domain.Framework.Models;

namespace Pumpkin.Domain.Framework.Services.Handlers;

public abstract class ApplicationCommandHandler<TRequest> : AsyncRequestHandler<TRequest>, IHandler
    where TRequest : IApplicationCommand
{
    public ILog Logger { get; }

    public ApplicationCommandHandler(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<CommandModelBase>();
    }
}