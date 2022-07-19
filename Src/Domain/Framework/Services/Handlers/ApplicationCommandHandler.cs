using Domain.Framework.Logging;
using Domain.Framework.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Domain.Framework.Services.Handlers;

public abstract class ApplicationCommandHandler<TRequest> : AsyncRequestHandler<TRequest>, IHandler
    where TRequest : IApplicationCommand
{
    public ILog Logger { get; }

    public ApplicationCommandHandler(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<CommandModelBase>();
    }
}