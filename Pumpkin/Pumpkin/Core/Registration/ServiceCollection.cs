using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Data;

namespace Pumpkin.Core.Registration
{
    public static partial class Extensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"))
                    .EnableDetailedErrors();
            });
        }
    }
}