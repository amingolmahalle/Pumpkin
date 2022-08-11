using MediatR;

namespace Pumpkin.Domain.Framework.Services;

public interface IApplicationCommand : IRequest
{
}

public interface IApplicationCommand<out TResult> : IRequest<TResult>
{
}