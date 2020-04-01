using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Data;

namespace Pumpkin.Web
{
    public static class ServiceConfiguration
    {
        public static void AddDbContextExtension(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });
        }
    }
}