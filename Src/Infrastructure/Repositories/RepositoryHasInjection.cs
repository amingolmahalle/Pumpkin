using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Infrastructure.Repositories;

public class RepositoryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:

        //Commands:
        // collection.AddScoped<IWeatherCommandRepository, WeatherCommandRepository>();
    }
}