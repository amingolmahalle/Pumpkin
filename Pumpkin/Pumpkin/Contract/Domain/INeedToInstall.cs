using Microsoft.Extensions.DependencyInjection;

namespace Pumpkin.Contract.Domain
{
    public interface INeedToInstall
    {
        void Install(IServiceCollection services);
    }
}