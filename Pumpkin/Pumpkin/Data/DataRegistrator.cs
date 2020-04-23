using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Contract.Registration;
using Pumpkin.Data.Listeners;
using Pumpkin.Data.Repositories;

namespace Pumpkin.Data
{
    public class DataRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            services.AddTransient<IBeforeInsertListener, HistoryBeforeInsert>();
            services.AddTransient<IBeforeUpdateListener, HistoryBeforeUpdate>();
            services.AddTransient<IBeforeDeleteListener, HistoryBeforeDelete>();
        }
    }
}