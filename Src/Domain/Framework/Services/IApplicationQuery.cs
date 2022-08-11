using MediatR;

namespace Pumpkin.Domain.Framework.Services;

public interface IApplicationQuery<out TResult> : IRequest<TResult>
{
        
}