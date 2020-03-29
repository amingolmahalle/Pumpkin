using Microsoft.Extensions.Configuration;
using Pumpkin.Web.Hosting;

namespace Sample.Test
{
    public class Startup :RootStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}