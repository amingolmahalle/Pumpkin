using MediatR;

namespace Domain.Framework.Services;

public interface IApplicationQuery<out TResult> : IRequest<TResult>
{
        
}