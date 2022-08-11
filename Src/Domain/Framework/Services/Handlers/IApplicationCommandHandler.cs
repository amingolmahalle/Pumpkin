using MediatR;
using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Services.Handlers;

public interface IApplicationCommandHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult>
    where TRequest : IApplicationCommand<TResult>
{
    public ILog Logger { get; }
}