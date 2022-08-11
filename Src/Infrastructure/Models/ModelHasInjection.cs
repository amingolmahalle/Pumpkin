using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Infrastructure.Models;

public class ModelHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Queries:

        //Commands:
        // collection.AddScoped<IWeatherCommandModel, WeatherCommandModel>();
    }
}