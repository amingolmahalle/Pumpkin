using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Pumpkin.Web.Hosting;

namespace Sample.Test
{
    public class Program : RootProgram<Startup>
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

    }
}