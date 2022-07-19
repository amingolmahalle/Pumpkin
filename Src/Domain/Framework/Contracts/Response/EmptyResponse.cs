using System.Diagnostics;
using System.Net;

namespace Framework.Contracts.Response;

public class EmptyResponse
{
    public const string Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public const bool Successful = true;

    public string Level { get; protected init; }
    public string Title { get; protected init; }
    public string Key { get; protected init; }
    public string Description { get; protected init; }
    public HttpStatusCode HttpCode { get; protected init; }
    public int SituationCode { get; protected init; }
    public string TraceId { get; protected init; }

    public static EmptyResponse Instance(string title = null, string description = null, string traceId = null, int situationCode = 0, 
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}

public class ListResponse : EmptyResponse
{
    public new static ListResponse Instance(string title = null, string description = null, string traceId = null, int situationCode = 0, 
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}

public class PagedResponse : EmptyResponse
{
    public new static PagedResponse Instance(string title = null, string description = null, string traceId = null, int situationCode = 0, 
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}