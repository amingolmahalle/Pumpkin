using MediatR;

namespace Pumpkin.Domain.Framework.Services.Handlers;

public interface IApplicationQueryHandler<in TRequest, TResult> : IRequestHandler<TRequest, TResult> 
    where TRequest : IApplicationQuery<TResult>
{
}