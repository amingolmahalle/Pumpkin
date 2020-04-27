using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Sample.Test.Data.Repositories;
using Sample.Test.Domain.Entity.UserAggregate;

namespace Sample.Test.Data
{
    public class DataRegistrator: INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
           services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}