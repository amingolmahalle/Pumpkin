using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Repositories.Order;
using Pumpkin.Infrastructure.Repositories.Order;

namespace Pumpkin.Infrastructure.Repositories;

public class RepositoryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:
        collection.AddScoped<IOrderQueryRepository, OrderQueryRepository>();

        //Commands:
        collection.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
    }
}