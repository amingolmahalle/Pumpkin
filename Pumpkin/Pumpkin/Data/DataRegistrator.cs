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
            // System.Reflection.Assembly.GetExecutingAssembly()
            //     .GetTypes()
            //     .Where(item => item.GetInterfaces()
            //         .Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == typeof(IRepository<,,>)) && !item.IsAbstract && !item.IsInterface)
            //     .ToList()
            //     .ForEach(assignedTypes =>
            //     {
            //         var serviceType = assignedTypes.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IRepository<,,>));
            //         services.AddScoped(serviceType, assignedTypes);
            //     });
            
            services.AddTransient<IBeforeInsertListener, HistoryBeforeInsert>();
            services.AddTransient<IBeforeUpdateListener, HistoryBeforeUpdate>();
            services.AddTransient<IBeforeDeleteListener, HistoryBeforeDelete>();
        }
    }
}