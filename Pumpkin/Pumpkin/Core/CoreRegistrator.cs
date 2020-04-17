using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Serialization;
using Pumpkin.Contract.Transaction;
using Pumpkin.Core.Serialization;
using Pumpkin.Core.Transaction;
using Pumpkin.Data;

namespace Pumpkin.Core
{
    public class CoreRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddTransient<ISerializer, NewtonSoftSerializer>();

            services.AddScoped<ITransactionService, TransactionService<DatabaseContext>>();
        }
    }
}