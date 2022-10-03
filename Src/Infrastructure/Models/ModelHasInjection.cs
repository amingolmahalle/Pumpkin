using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Models.Policy;
using Pumpkin.Infrastructure.Models.Policy;

namespace Pumpkin.Infrastructure.Models;

public class ModelHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:

        //Commands:
        collection.AddScoped<IPolicyQueryModel, PolicyQueryModel>();
    }
}