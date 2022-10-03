using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Repositories.Policy;
using Pumpkin.Infrastructure.Repositories.Policy;

namespace Pumpkin.Infrastructure.Repositories;

public class RepositoryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:
        collection.AddScoped<IPolicyQueryRepository, PolicyQueryRepository>();

        //Commands:
        collection.AddScoped<IPolicyCommandRepository, PolicyCommandRepository>();
    }
}