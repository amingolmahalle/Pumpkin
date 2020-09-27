using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Pumpkin.Common.Helpers;
using Pumpkin.Web.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sample.Test.Web.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomSwagger(this IServiceCollection services)
        {
            Helpers.NotNull(services, nameof(services));

            #region AddSwaggerExamples

            //Add services to use Example Filters in swagger
            //If you want to use the Request and Response example filters (and have called options.ExampleFilters() above), then you MUST also call
            //This method to register all ExamplesProvider classes form the assembly
            //services.AddSwaggerExamplesFromAssemblyOf<PersonRequestExample>();

            //We call this method for by reflection with the Startup type of entry assembly (MyApi assembly)
            var mainAssembly = Assembly.GetEntryAssembly(); // => MyApi project assembly
            var mainType = mainAssembly!.GetExportedTypes()[0];

            var methodName = nameof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions
                .AddSwaggerExamplesFromAssemblyOf);

            //MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetMethod(methodName);
            MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetRuntimeMethods()
                .FirstOrDefault(x => x.Name == methodName && x.IsGenericMethod);

            MethodInfo generic = method!.MakeGenericMethod(mainType);
            generic.Invoke(null, new object[] {services});

            #endregion

            //Add services and configuration to use swagger
            services.AddSwaggerGen(options =>
            {
                var xmlDocPath = Path.Combine(AppContext.BaseDirectory, "Document.xml");
                //show controller XML comments like summary
                options.IncludeXmlComments(xmlDocPath, true);

                options.EnableAnnotations();

                #region DescribeAllEnumsAsStrings

                //This method was Deprecated. 
                options.DescribeAllEnumsAsStrings();

                #endregion

                options.DescribeAllParametersInCamelCase();
                options.DescribeStringEnumsInCamelCase();
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();

                options.SwaggerDoc("v1", new OpenApiInfo {Version = "v1", Title = "API V1"});
                options.SwaggerDoc("v2", new OpenApiInfo {Version = "v2", Title = "API V2"});

                #region Filters

                //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
                options.ExampleFilters();

                //It doesn't work anymore in recent versions because of replacing Swashbuckle.AspNetCore.Examples with Swashbuckle.AspNetCore.Filters
                //Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
                //options.OperationFilter<AddFileParamTypesOperationFilter>();

                //Set summary of action if not already set
                options.OperationFilter<ApplySummariesOperationFilter>();

                #region Add UnAuthorized to Response

                //Add 401 response and security requirements (Lock icon) to actions that need authorization
                options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");

                #endregion

                //OAuth2Scheme
                options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
                {
                    //Scheme = "Bearer",
                    //In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("https://localhost:5001/api/v1/users/Token"),
                        }
                    }
                });

                #endregion

                #region Versioning

                // Remove version parameter from all Operations
                options.OperationFilter<RemoveVersionParameters>();

                //set version "api/v{version}/[controller]" from current swagger doc verion
                options.DocumentFilter<SetVersionInPaths>();

                //Seperate and categorize end-points by doc version
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo))
                        return false;

                    var versions = methodInfo.DeclaringType!
                        .GetCustomAttributes<ApiVersionAttribute>(true)
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v.ToString()}" == docName);
                });

                #endregion
            });
        }
    }
}