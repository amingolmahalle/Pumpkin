using MediatR;

namespace Domain.Framework.Services;

public interface IApplicationCommand : IRequest
{
}

public interface IApplicationCommand<out TResult> : IRequest<TResult>
{
}