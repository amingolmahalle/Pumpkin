using System.Diagnostics;
using System.Net;

namespace Framework.Contracts.Response;

public class DataResponse<TData>
{
    public const string Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public string Level { get; protected init; } = "INFO";
    public string Key { get; protected init; } = "SUCCESS";
    public string Title { get; protected init; }
    public string Description { get; protected init; }
    public HttpStatusCode HttpCode { get; protected init; } = HttpStatusCode.OK;
    public int SituationCode { get; protected init; }
    public string TraceId { get; protected init; }
    public TData Data { get; protected init; }

    public EmptyResponse AsEmptyResponse()
        => EmptyResponse.Instance(Title, Description, TraceId, SituationCode, HttpCode, Key, Level);

    public static DataResponse<TData> Instance(TData data, string title = null, string description = null, string traceId = null, int situationCode = 0,
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Data = data,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}

public class ListResponse<TData> : DataResponse<IEnumerable<TData>>
{
    public new static ListResponse<TData> Instance(IEnumerable<TData> data, string title = null, string description = null, string traceId = null, int situationCode = 0,
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Data = data,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}

public class PagedResponse<TData> : DataResponse<PageableData<TData>>
{
    public new static PagedResponse<TData> Instance(PageableData<TData> data, string title = null, string description = null, string traceId = null, int situationCode = 0,
        HttpStatusCode httpCode = HttpStatusCode.OK, string key = "SUCCESS", string level = "INFO")
        => new()
        {
            Level = level,
            Key = key,
            Data = data,
            Title = title,
            Description = description,
            TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId,
            HttpCode = httpCode,
            SituationCode = situationCode
        };
}