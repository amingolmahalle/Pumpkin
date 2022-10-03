using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Application.Commands;

public class ApplicationCommandHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        // collection.AddScoped<IWeatherCommands, WeatherCommands>();
    }
}