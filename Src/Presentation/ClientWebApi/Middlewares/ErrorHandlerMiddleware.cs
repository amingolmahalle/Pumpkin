using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Framework.Exceptions;
using Infrastructure.Framework.HttpResults.Contracts;

namespace ClientWebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly bool _isDevelopment;
    private readonly bool _logErrorsToConsole;
    private readonly bool _logErrorsToDebug;

    public ErrorHandlerMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        _next = next;
        _isDevelopment = environment.IsDevelopment() || environment.IsEnvironment("Staging");
        _logErrorsToConsole = !string.IsNullOrEmpty(configuration["Logging:Console:Errors"]) && bool.Parse(configuration["Logging:Console:Errors"]);
        _logErrorsToDebug = !string.IsNullOrEmpty(configuration["Logging:Debug:Errors"]) && bool.Parse(configuration["Logging:Debug:Errors"]);
    }

    public async Task Invoke(HttpContext context, IHttpContextAccessor accessor)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await Handle(context, accessor, exception);
        }
    }

    private async Task Handle(HttpContext httpContext, IHttpContextAccessor accessor, Exception ex)
    {
        httpContext.Response.ContentType = "application/problem+json";
        var payload = new Dictionary<string, string>();

        #region < ERROR RESPONSE >

        // Get the details to display, depending on whether we want to expose the raw exception
        var key = "UNHANDLED_ERROR";
        var title = _isDevelopment ? "خطای پیش بینی نشده: " + ex.Message : "سرویس با خطا مواجه شد";
        var description = _isDevelopment ? ex.StackTrace : null;

        // This is often very handy information for tracing the specific request
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        var httpCode = HttpStatusCode.InternalServerError;
        int? situationCode = null;
        var errors = new Dictionary<string, object>();

        if (ex is Dexception dex)
        {
            key = dex.Key;
            title = dex.Message;
            description = _isDevelopment
                ? string.IsNullOrWhiteSpace(dex.Description)
                    ? description
                    : dex.Description + "\n" + description
                : dex.Description;
            httpCode = dex.HttpCode;
            situationCode = dex.SituationCode;
            traceId = string.IsNullOrWhiteSpace(dex.TraceId) ? traceId : dex.TraceId;

            payload.Add("DexTitle", dex.Key);
            payload.Add("DexMessage", dex.Message);
            payload.Add("DexDescription", dex.Description);
            payload.Add("DexHttpCode", ((int) dex.HttpCode) + " " + dex.HttpCode.ToString());
            payload.Add("DexStatus", dex.SituationCode.ToString());
            payload.Add("DexTraceId", traceId);
        }

        var i = 1;
        var iex = ex.InnerException;
        while (iex != null)
        {
            errors.Add($"خطای داخلی شماره {i++} : ", iex.Message + "\n\n" + iex.StackTrace);
            iex = iex.InnerException;
        }

        #endregion

        // new ElkLogConfig().CreateLogger(GlobalConfig.Config, accessor)
        //     .LogError("Application encounter an error", ex, payload, "error-log", "error-handling");

        var problem = new StandardForcedResponseContract
        {
            Result = new StandardForcedResultContract
            {
                Title = key,
                Message = title,
                Description = description,
                Status = situationCode ?? 0,
                TraceId = traceId,
                Errors = errors
            }
        };

        httpContext.Response.StatusCode = (int) httpCode;
        var stream = httpContext.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problem, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}

public static class ErrorHandlerExtensions
{
    public static void UseCustomErrorHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}