using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Pumpkin.Common;
using Pumpkin.Common.Extensions;
using Pumpkin.Contract.Logging;
using Pumpkin.Contract.Security;
using Pumpkin.Web.Filters.Validator.Dto;
using Pumpkin.Web.ResponseWrapper;

namespace Pumpkin.Web.RequestWrapper
{
    public class ApiRequestInterceptor
    {
        private readonly RequestDelegate _next;

        private ICurrentRequest _currentRequest;

        private IWebHostEnvironment _environment;

        private readonly ILog _logger = LogManager.GetLogger(nameof(ApiRequestInterceptor));

        public ApiRequestInterceptor(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext context,
            ICurrentRequest currentRequest,
            IWebHostEnvironment environment)
        {
            if (IsSwagger())
                await _next(context);

            else
            {
                _currentRequest = currentRequest;
                _environment = environment;

                InterceptRequest();

                var originalBodyStream = context.Response.Body;

                await using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                try
                {
                    await _next.Invoke(context);

                    switch (context.Response.StatusCode)
                    {
                        case (int) HttpStatusCode.OK:
                        {
                            var body = await FormatResponse(context.Response);

                            await HandleSuccessRequestAsync(body);
                            break;
                        }
                        case Constants.FluentValidationHttpStatusCode:
                        {
                            var body = await FormatResponse(context.Response);

                            await HandleNotSuccessRequestAsync(body,
                                Constants.FluentValidationHttpStatusCode);
                            break;
                        }
                        default:
                        {
                            await HandleNotSuccessRequestAsync(null, context.Response.StatusCode);
                            break;
                        }
                    }
                }
                catch (ApiException ex)
                {
                    _logger.Error(ex.Message, ex);

                    await HandleValidationErrorAsync(ex);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message, ex);

                    await HandleExceptionAsync(ex);
                }

                finally
                {
                    responseBody.Seek(0, SeekOrigin.Begin);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }

            void InterceptRequest()
            {
                foreach (var header in context.Request.Headers.Where(it => it.Key.ToLower().StartsWith("request")))
                {
                    _currentRequest.Headers[header.Key.ToLower()] = header.Value;
                }

                if (!_currentRequest.Headers.ContainsKey("request-gateway"))
                    throw new ApiException(HttpStatusCode.BadRequest, $"empty header detected [request-gateway]");

                _currentRequest.Gateway = _currentRequest.GetEnumHeader<GatewayType>("request-gateway");
                _currentRequest.UserSessionId = _currentRequest.GetHeader("request-client-id");
                _currentRequest.CorrelationId = Guid.NewGuid().ToString();

                if (string.IsNullOrEmpty(_currentRequest.UserSessionId))
                    throw new ApiException(HttpStatusCode.BadRequest, $"empty header detected [request-client-id]");

                if (context.User.Identity.IsAuthenticated)
                {
                    _currentRequest.UserId = int.Parse(context.User.FindFirst("sub").Value);
                    _currentRequest.UserName = context.User.FindFirst("name").Value;

                    switch (context.User.FindFirst("amr").Value)
                    {
                        case "otp":
                            _currentRequest.AuthenticationType = AuthenticationType.OtpAuthentication;
                            break;
                        case "password":
                        case "pwd":
                            _currentRequest.AuthenticationType = AuthenticationType.PasswordAuthentication;
                            break;
                    }
                }
                else
                {
                    _currentRequest.AuthenticationType = AuthenticationType.NotAuthenticated;
                }

                context.Items.Add("CoreRequest", _currentRequest);
            }

            async Task HandleExceptionAsync(Exception exception)
            {
                ApiError apiError;

                int statusCode = (int) HttpStatusCode.InternalServerError;

                if (exception is UnauthorizedAccessException)
                {
                    apiError = new ApiError(ResponseMessageEnum.UnAuthorized.GetDescription());
                    statusCode = (int) HttpStatusCode.Unauthorized;
                }
                else
                {
                    var exceptionMessage = ResponseMessageEnum.Unhandled.GetDescription();

                    string message;
                    if (_environment.IsDevelopment())
                    {
                        var dict = new Dictionary<string, string>
                        {
                            ["Exception"] = $"{exceptionMessage} => {exception.Message}",
                            ["StackTrace"] = exception.StackTrace,
                        };
                        message = JsonConvert.SerializeObject(dict);
                    }
                    else
                    {
                        message = exception.Message;
                    }

                    apiError = new ApiError(message);
                }

                await WriteToResponseAsync(null, apiError, statusCode, ResponseMessageEnum.Exception);
            }

            async Task HandleValidationErrorAsync(ApiException exception)
            {
                string message;

                if (_environment.IsDevelopment())
                {
                    var dict = new Dictionary<string, string>
                    {
                        ["Exception"] = exception.Message,
                        ["StackTrace"] = exception.StackTrace,
                    };

                    if (exception.InnerException != null)
                    {
                        dict.Add("InnerException.Exception", exception.InnerException.Message);
                        dict.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }

                    if (exception.AdditionalDataList != null)
                        dict.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalDataList));

                    message = JsonConvert.SerializeObject(dict);
                }
                else
                {
                    message = exception.Message;
                }

                var apiError = new ApiError(message, exception.AdditionalDataList);

                await WriteToResponseAsync(
                    null,
                    apiError,
                    (int) exception.HttpStatusCode,
                    ResponseMessageEnum.Exception);
            }

            async Task HandleNotSuccessRequestAsync(string body, int code)
            {
                ApiError apiError;
                var errorMessage = string.Empty;

                switch (code)
                {
                    case (int) HttpStatusCode.NotFound:
                        apiError = new ApiError(ResponseMessageEnum.NotFound.GetDescription());
                        break;
                    case (int) HttpStatusCode.NoContent:
                        apiError = new ApiError(ResponseMessageEnum.NotContent.GetDescription());
                        break;
                    case (int) HttpStatusCode.MethodNotAllowed:
                        apiError = new ApiError(ResponseMessageEnum.MethodNotAllowed.GetDescription());
                        break;
                    case (int) HttpStatusCode.BadRequest:
                        apiError = new ApiError(ResponseMessageEnum.BadRequest.GetDescription());
                        break;
                    case Constants.FluentValidationHttpStatusCode:
                        JsonConvert.DeserializeObject<ErrorFluentValidation>(body).Errors
                            .ForEach(efv => errorMessage += $"{efv}, ");
                        apiError = new ApiError(
                            $"{ResponseMessageEnum.ValidationError.GetDescription()} => {errorMessage.Remove(errorMessage.Length - 1)}");
                        break;
                    default:
                        apiError = new ApiError(ResponseMessageEnum.Unknown.GetDescription());
                        break;
                }

                await WriteToResponseAsync(null, apiError, code, ResponseMessageEnum.Failure);
            }

            async Task HandleSuccessRequestAsync(string body)
            {
                var bodyText = !body.IsValidJson() ? JsonConvert.SerializeObject(body) : body;

                dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);

                await WriteToResponseAsync(
                    bodyContent,
                    null,
                    (int) HttpStatusCode.OK,
                    ResponseMessageEnum.Success);
            }

            async Task WriteToResponseAsync(
                object result,
                ApiError apiError,
                int httpStatusCode,
                ResponseMessageEnum apiStatusCode)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException(
                        "The response has already started, the http status code middleware will not be executed.");

                var apiResponse = new ApiResponse(httpStatusCode, apiStatusCode.GetDescription(), result, apiError);

                var json = JsonConvert.SerializeObject(apiResponse);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = httpStatusCode;

                await context.Response.WriteAsync(json);
            }

            async Task<string> FormatResponse(HttpResponse response)
            {
                response.Body.Seek(0, SeekOrigin.Begin);

                var plainBodyText = await new StreamReader(response.Body).ReadToEndAsync();

                response.Body.Seek(0, SeekOrigin.Begin);
                response.Body.SetLength(0);

                return plainBodyText;
            }

            bool IsSwagger()
            {
                return context.Request.Path.ToString().Contains("/swagger") ||
                       context.Request.Path.ToString().Contains("/index.html");
            }
        }
    }
}