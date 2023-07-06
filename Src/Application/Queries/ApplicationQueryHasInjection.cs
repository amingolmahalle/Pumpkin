using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Application.Queries.Order;
using Pumpkin.Domain.Application.Queries.Policy;
using Pumpkin.Domain.Framework.Services;

namespace Pumpkin.Application.Queries;

public class ApplicationQueryHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddScoped<IOrderQueries, OrderQueries>();
    }
}