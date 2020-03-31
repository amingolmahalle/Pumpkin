using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Registration;
using Pumpkin.Data.Repositories;

namespace Pumpkin.Data
{
    public class DataRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}