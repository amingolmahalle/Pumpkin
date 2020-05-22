using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pumpkin.Contract.Logging;
using Pumpkin.Contract.Security;
using Pumpkin.Core.ResponseWrapper;
using Pumpkin.Utils;
using Pumpkin.Utils.Extensions;
using Pumpkin.Web.Filters.Validator.Dto;

namespace Pumpkin.Web.RequestWrapper
{
    public class ApiRequestInterceptor
    {
        private readonly RequestDelegate _next;

        private ICurrentRequest _currentRequest;

        public ApiRequestInterceptor(RequestDelegate next)
        {
            _next = next;
        }

        private readonly ILog _logger = LogManager.GetLogger("RequestInterceptor");
        public async Task InvokeAsync(HttpContext context, ICurrentRequest currentRequest)
        {
            if (IsSwagger(context))
                await _next(context);

            else
            {
                _currentRequest = currentRequest;

                InterceptRequest(context);

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next.Invoke(context);

                        switch (context.Response.StatusCode)
                        {
                            case (int) HttpStatusCode.OK:
                            {
                                var body = await FormatResponse(context.Response);

                                await HandleSuccessRequestAsync(context, body, context.Response.StatusCode);
                                break;
                            }
                            case Constants.FluentValidationHttpStatusCode:
                            {
                                var body = await FormatResponse(context.Response);

                                await HandleNotSuccessRequestAsync(context, body,
                                    Constants.FluentValidationHttpStatusCode);
                                break;
                            }
                            default:
                            {
                                await HandleNotSuccessRequestAsync(context, null, context.Response.StatusCode);
                                break;
                            }
                        }
                    }
                    catch (ApiException ex)
                    {
                        _logger.Error(ex.Message, ex);
                        
                        await HandleValidationErrorAsync(context, ex);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.Message, ex);
                        
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

        private void InterceptRequest(HttpContext context)
        {
            foreach (var header in context.Request.Headers.Where(it => it.Key.ToLower().StartsWith("request")))
            {
                _currentRequest.Headers[header.Key.ToLower()] = header.Value;
            }

            if (!_currentRequest.Headers.ContainsKey("request-gateway"))
                throw new Exception($"empty header detected [request-gateway]");

            _currentRequest.Gateway = _currentRequest.GetEnumHeader<GatewayType>("request-gateway");
            _currentRequest.UserSessionId = _currentRequest.GetHeader("request-client-id");
            _currentRequest.CorrelationId = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(_currentRequest.UserSessionId))
                throw new Exception($"empty header detected [request-client-id]");

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
                apiError = new ApiError(ResponseMessageEnum.UnAuthorized.GetDescription());
                code = (int) HttpStatusCode.Unauthorized;
                context.Response.StatusCode = code;
            }
            else
            {
                var exceptionMessage = ResponseMessageEnum.Unhandled.GetDescription();
#if !DEBUG
                var message = exceptionMessage;
                string stackTrace = null;
#else
                var message = $"{exceptionMessage} {exception.GetBaseException().Message}";
                string stackTrace = exception.StackTrace;
#endif

                apiError = new ApiError(message)
                {
                    Details = stackTrace
                };
                code = (int) HttpStatusCode.InternalServerError;
                context.Response.StatusCode = code;
            }

            context.Response.ContentType = "application/json";

            var apiResponse = new ApiResponse(code, ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleValidationErrorAsync(HttpContext context, ApiException exception)
        {
            var ex = exception;

            var apiError = new ApiError(ResponseMessageEnum.ValidationError.GetDescription(), ex.Errors)
            {
                ValidationErrors = ex.Errors,
                ReferenceErrorCode = ex.ReferenceErrorCode,
                ReferenceDocumentLink = ex.ReferenceDocumentLink
            };

            context.Response.StatusCode = (int) HttpStatusCode.OK;

            context.Response.ContentType = "application/json";

            var apiResponse = new ApiResponse((int) HttpStatusCode.BadRequest,
                ResponseMessageEnum.Exception.GetDescription(), null, apiError);

            var json = JsonConvert.SerializeObject(apiResponse);

            return context.Response.WriteAsync(json);
        }

        private static Task HandleNotSuccessRequestAsync(HttpContext context, string body, int code)
        {
            context.Response.ContentType = "application/json";

            ApiError apiError;
            string errorMessage = string.Empty;

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
                        .ForEach(efv => errorMessage += $"{efv},");
                    apiError = new ApiError(errorMessage.Remove(errorMessage.Length - 1));
                    break;
                default:
                    apiError = new ApiError(ResponseMessageEnum.Unknown.GetDescription());
                    break;
            }

            var apiResponse = new ApiResponse(code, ResponseMessageEnum.Failure.GetDescription(), null, apiError);

            context.Response.StatusCode = code;

            var jsonString = JsonConvert.SerializeObject(apiResponse);
            return context.Response.WriteAsync(jsonString);
        }

        private static Task HandleSuccessRequestAsync(HttpContext context, string body, int code)
        {
            context.Response.ContentType = "application/json";

            var bodyText = !body.IsValidJson() ? JsonConvert.SerializeObject(body) : body;

            dynamic bodyContent = JsonConvert.DeserializeObject<dynamic>(bodyText);

            var apiResponse = new ApiResponse(code, ResponseMessageEnum.Success.GetDescription(), bodyContent, null);

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

        private static bool IsSwagger(HttpContext context)
        {
            return context.Request.Path.ToString().Contains("/swagger") ||
                   context.Request.Path.ToString().Contains("/index.html");
        }
    }
}