using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Domain.Framework.Services;
using Pumpkin.Infrastructure.Contexts.Providers;
using Pumpkin.Infrastructure.Contexts.Providers.InMemory;
using Pumpkin.Infrastructure.Contexts.Providers.SqlServer;

namespace Pumpkin.Infrastructure.Contexts;

public class ContextHasInjection : IHaveInjection
{
    public void Inject(IServiceCollection collection, IConfiguration configuration)
    {
        #region << C O N N E C T I O N   S T R I N G >>

        var context = "ORIGINAL";
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("CURRENT_CONTEXT")))
            context = Environment.GetEnvironmentVariable("CURRENT_CONTEXT");

        switch (context)
        {
           case "ORIGINAL":
                collection.AddDbContext<SqlServerQueryDbContext>(ServiceLifetime.Transient);
                collection.AddScoped<QueryDbContext, SqlServerQueryDbContext>();
                
                collection.AddDbContext<SqlServerCommandDbContext>(ServiceLifetime.Transient);
                collection.AddScoped<CommandDbContext, SqlServerCommandDbContext>();
                break;

            case "NONE":
                collection.AddDbContext<MemoryQueryDbContext>();
                collection.AddScoped<QueryDbContext, MemoryQueryDbContext>();

                collection.AddDbContext<MemoryCommandDbContext>();
                collection.AddScoped<CommandDbContext, MemoryCommandDbContext>();
                break;
        }

        #endregion
    }
}