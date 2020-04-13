using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Pumpkin.Contract.Transaction;
using Pumpkin.Core.Transaction;
using Pumpkin.Data;

namespace Pumpkin.Core
{
    public class CoreRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped<ITransactionService, TransactionService<DatabaseContext>>();
        }
    }
}