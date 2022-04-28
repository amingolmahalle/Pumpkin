using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Security;
using Pumpkin.Web.Authorization;

namespace Pumpkin.Web;

public class WebRegistrator : INeedToInstall
{
    public void Install(IServiceCollection services)
    {
        services.AddScoped<ICurrentRequest, CurrentRequest>();
    }
}