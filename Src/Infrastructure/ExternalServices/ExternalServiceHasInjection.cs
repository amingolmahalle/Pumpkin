using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Infrastructure.ExternalServices.Helpers.Polly.CircuitBreaker;

namespace Pumpkin.Infrastructure.ExternalServices;

public class ExternalServiceHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        // collection.AddHttpClient<WeatherInformationServiceClient>("WeatherService")
        //     .AddPolicyHandler(PollyHelper.GetCircuitBreakerPolicy(0.9, 10, 2, 30));
        //
        // collection.AddScoped<IWeatherInformationServiceClient, WeatherInformationServiceClient>();
    }
}