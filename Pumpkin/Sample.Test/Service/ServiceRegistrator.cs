using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Registration;
using Sample.Test.Domain.Service.Commands.AddUser;
using Sample.Test.Domain.Service.Commands.EditUser;
using Sample.Test.Domain.Service.Queries.GetUserById;
using Sample.Test.Service.Commands.AddUser;
using Sample.Test.Service.Commands.EditUser;
using Sample.Test.Service.Queries.GetUserById;

namespace Sample.Test.Service
{
    public class ServiceRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            services.AddScoped<IGetUserByIdService, GetUserByIdService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IEditUserService, EditUserService>();
        }
    }
}