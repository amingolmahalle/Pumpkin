using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Security;
using Pumpkin.Web.Authorization;

namespace Pumpkin.Web
{
    public class WebRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped(typeof(ICurrentRequest), typeof(CurrentRequest));
        }
    }
}