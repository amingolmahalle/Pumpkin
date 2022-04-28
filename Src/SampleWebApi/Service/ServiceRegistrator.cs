using FluentValidation;
using Pumpkin.Contract.Domain;
using SampleWebApi.Domain.Service.Commands.AddUser;
using SampleWebApi.Domain.Service.Commands.EditUser;
using SampleWebApi.Domain.Service.Queries.GetUserById;
using SampleWebApi.Domain.Service.Queries.GetUserByMobile;
using SampleWebApi.Service.Commands.AddUser;
using SampleWebApi.Service.Commands.EditUser;
using SampleWebApi.Service.Queries.GetUserById;
using SampleWebApi.Service.Queries.GetUserByMobile;

namespace SampleWebApi.Service;

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