using Domain.Framework.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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