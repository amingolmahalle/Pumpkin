using Microsoft.Extensions.Configuration;

namespace Pumpkin.Infrastructure.ExternalServices;

public abstract class WeatherBaseEndpoint
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    
    private static readonly object SyncLock = new();

    public WeatherBaseEndpoint(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _configuration = configuration.GetSection("WebServices");
        _httpClientFactory = httpClientFactory;
    }

    private HttpClient _connection;

    protected HttpClient Connection
    {
        get
        {
            lock (SyncLock)
            {
                _connection = _httpClientFactory.CreateClient("WeatherService");
                _connection.Timeout = TimeSpan.FromSeconds(5);
                _connection.BaseAddress = new Uri(_configuration["weather-info:ApiUrl"]);
                _connection.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

                return _connection;
            }
        }
    }
}