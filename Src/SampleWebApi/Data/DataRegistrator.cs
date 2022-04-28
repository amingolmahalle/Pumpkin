using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using SampleWebApi.Data.Repositories;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Data
{
    public class DataRegistrator: INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
           services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}