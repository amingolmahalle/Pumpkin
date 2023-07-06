using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Domain.Models.Order;
using Pumpkin.Domain.Models.Profile;
using Pumpkin.Infrastructure.Models.Order;
using Pumpkin.Infrastructure.Models.Profile;

namespace Pumpkin.Infrastructure.Models;

public class ModelHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        //Commands:
        collection.AddScoped<IOrderCommandModel, OrderCommandModel>();
        collection.AddScoped<IUserCommandModel, UserCommandModel>();

        //Queries:
        collection.AddScoped<IOrderQueryModel, OrderQueryModel>();
    }
}