using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pumpkin.Contract.Security;
using Pumpkin.Core.RequestWrapper;
using Pumpkin.Utils;
using Pumpkin.Web.Authorization;

namespace Pumpkin.Web.RequestWrapper
{
    public class ApiRequestInterceptor
    {
        private readonly RequestDelegate _next;

        private readonly IServiceProvider _serviceProvider;

        public ApiRequestInterceptor(IServiceProvider serviceProvider, RequestDelegate next)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        //   private ILog logger = LogManager.GetLogger("RequestInterceptor");

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (IgnoreInterception(context))
                    await _next(context);

                else
                {
                    InterceptRequest(context);

                    var originalBodyStream = context.Response.Body;

                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        try
                        {
                            await _next.Invoke(context);

                            if (context.Response.StatusCode == (int) HttpStatusCode.OK)
                            {
                                var body = await FormatResponse(context.Response);

                                await HandleSuccessRequestAsync(context, body, context.Response.StatusCode);
                            }
                            else
                            {
                                await HandleNotSuccessRequestAsync(context, context.Response.StatusCode);
                            }
                        }
                        catch (ApiException ex)
                        {
                            //     logger.Error(ex.Message, ex);
                            await HandleValidationErrorAsync(context, ex);
                        }
                        catch (Exception ex)
                        {
                            //   logger.Error(ex.Message, ex);
                            await HandleExceptionAsync(context, ex);
                        }

                        finally
                        {
                            responseBody.Seek(0, SeekOrigin.Begin);

                            await responseBody.CopyToAsync(originalBodyStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // logger.Error(ex.Message, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private void InterceptRequest(HttpContext context)
        {
            // var service = new ServiceCollection()
            //     .AddScoped<CurrentRequest, CurrentRequest>();
            //
            // var serviceProvider = service.BuildServiceProvider();

            var currentRequest = _serviceProvider.GetService<CurrentRequest>();

            foreach (var header in context.Request.Headers.Where(it => it.Key.ToLower().StartsWith("request")))
            {
                currentRequest.Headers[header.Key.ToLower()] = header.Value;
            }

            if (!currentRequest.HasHeader("request-gateway"))
                throw new Exception($"empty header detected [request-gateway]");

            currentRequest.Gateway = currentRequest.GetEnumHeader<GatewayType>("request-gateway");
            currentRequest.UserSessionId = currentRequest.GetHeader("request-client-id");
            currentRequest.CorrelationId = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(currentRequest.UserSessionId))
                throw new Exception($"empty header detected [request-client-id]");

            if (context.User.Identity.IsAuthenticated)
            {
                currentRequest.UserId = int.Parse(context.User.FindFirst("sub").Value);
                currentRequest.UserName = context.User.FindFirst("name").Value;

                switch (context.User.FindFirst("amr").Value)
                {
                    case "otp":
                        currentRequest.AuthenticationType = AuthenticationType.OtpAuthentication;
                        break;
                    case "password":
                    case "pwd":
                        currentRequest.AuthenticationType = AuthenticationType.PasswordAuthentication;
                        break;
                }
            }
            else
            {
                currentRequest.AuthenticationType = AuthenticationType.NotAuthenticated;
            }

            context.Items.Add("CoreRequest", currentRequest);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiError apiError;
            int code = 0;

            if (exception is ApiException ex)
            {
                apiError = new ApiError(ex.Message)
                {
                    ValidationErrors = ex.Errors,
                    ReferenceErrorCode = ex.ReferenceErrorCode,
                    ReferenceDocumentLink = ex.ReferenceDocumentLink
                };

                context.Response.StatusCode = code;
            }
            else if (exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                code = (int) HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
#if !DEBUG
                var msg = "An unhandled error occurred.";
                string stack = null;
#else
                var msg = exception.GetBaseException().Message;
                string stack = exception.StackTrace;
#endif

                apiError = new ApiError(msg)
                {
                    Details = stack
                };
                code = (int) HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";

            var apiResponse = new APIResponse(code, ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleValidationErrorAsync(HttpContext context, ApiException exception)
        {
            var ex = exception;

            var apiError = new ApiError(ex.Message)
            {
                ValidationErrors = ex.Errors,
                ReferenceErrorCode = ex.ReferenceErrorCode,
                ReferenceDocumentLink = ex.ReferenceDocumentLink
            };

            context.Response.StatusCode = (int) HttpStatusCode.OK;

            context.Response.ContentType = "application/json";

            var apiResponse = new APIResponse((int) HttpStatusCode.BadRequest,
                ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotSuccessRequestAsync(HttpContext context, int code)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError;

            switch (code)
            {
                case (int) HttpStatusCode.NotFound:
                    apiError = new ApiError("The specified URI does not exist. Please verify and try again.");
                    break;
                case (int) HttpStatusCode.NoContent:
                    apiError = new ApiError("The specified URI does not contain any content.");
                    break;
                default:
                    apiError = new ApiError("Your request cannot be processed. Please contact a support.");
                    break;
            }

            var apiResponse = new APIResponse(code, ResponseMessageEnum.Failure.GetDescription(), null, apiError);

            context.Response.StatusCode = code;

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleSuccessRequestAsync(HttpContext context, object body, int code)
        {
            context.Response.ContentType = "application/json";

            var bodyText = !body.ToString().IsValidJson() ? JsonConvert.SerializeObject(body) : body.ToString();

            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);

            var apiResponse = new APIResponse(code, ResponseMessageEnum.Success.GetDescription(), bodyContent, null);

            var jsonString = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(jsonString);
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);
            response.Body.SetLength(0);

            return plainBodyText;
        }

        private bool IgnoreInterception(HttpContext context)
        {
            return ApiRequestInterceptorExtension.Exceptions.Any(it => context.Request.Path.Value.Contains(it));
        }
    }
}