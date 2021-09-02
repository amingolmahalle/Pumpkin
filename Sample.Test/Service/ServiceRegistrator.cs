using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Sample.Test.Domain.Service.Commands.AddUser;
using Sample.Test.Domain.Service.Commands.EditUser;
using Sample.Test.Domain.Service.Queries.GetUserById;
using Sample.Test.Domain.Service.Queries.GetUserByMobile;
using Sample.Test.Service.Commands.AddUser;
using Sample.Test.Service.Commands.EditUser;
using Sample.Test.Service.Queries.GetUserById;
using Sample.Test.Service.Queries.GetUserByMobile;

namespace Sample.Test.Service
{
    public class ServiceRegistrator : INeedToInstall
    {
        public void Install(IServiceCollection services)
        {
            // Services
            services.AddScoped<IGetUserByIdService, GetUserByIdService>();
            services.AddScoped<IGetUserByMobileService, GetUserByMobileService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IEditUserService, EditUserService>();

            // Validator
            services.AddScoped<IValidator<AddUserRequest>, AddUserValidator>();
            services.AddScoped<IValidator<EditUserRequest>, EditUserValidator>();
            services.AddScoped<IValidator<GetUserByIdRequest>, GetUserByIdValidator>();
            services.AddScoped<IValidator<GetUserByMobileRequest>, GetUserByMobileValidator>();
        }
    }
}