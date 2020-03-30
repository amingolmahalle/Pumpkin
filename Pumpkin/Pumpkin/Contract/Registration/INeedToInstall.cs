using Microsoft.Extensions.DependencyInjection;

namespace Pumpkin.Contract.Registration
{
    public interface INeedToInstall
    {
        void Install(IServiceCollection services);
    }
}