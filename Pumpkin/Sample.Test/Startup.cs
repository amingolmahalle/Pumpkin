using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Core.Registration;
using Pumpkin.Web.Hosting;
using Sample.Test.Data;

namespace Sample.Test
{
    public class Startup : RootStartup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddDbContextEx<ApplicationDbContext>(
                _configuration.GetConnectionString("SqlServer"));
        }
    }
}