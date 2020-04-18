using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Serialization;
using Pumpkin.Core.Serialization;

namespace Pumpkin.Core
{
    public class CoreRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddTransient<ISerializer, NewtonSoftSerializer>();
        }
    }
}