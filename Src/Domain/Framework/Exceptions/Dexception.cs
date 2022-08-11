using System.Diagnostics;
using System.Net;
using Framework.Exceptions;

namespace Pumpkin.Domain.Framework.Exceptions;

public sealed class Dexception : Exception
{
    public const string Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public const bool Successful = false;


    public string LogProduct { get; set; } = "Unknown Product";
    public string LogApiName { get; set; } = "Unknown Service";
    public string LogApiVersion { get; set; } = "v0.0.0";


    public new string Message { get; private init; }
    public string Key { get; private init; }
    public string? Description { get; private init; }
    public HttpStatusCode HttpCode { get; private init; }
    public int? SituationCode { get; private init; }
    public string TraceId { get; private init; }
    public string Level { get; private init; }
    public Dictionary<string, string> Payload { get; private init; }

    public Dexception(Situation situation, string? description = null, string? traceId = null, Dictionary<string, string>? payload = null)
    {
        Message = situation.Message;
        Key = situation.Key;
        Description = description;
        HttpCode = situation.ResultCode;
        SituationCode = situation.Status;
        Level = situation.Level;
        Payload = payload ?? new Dictionary<string, string>();
        TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId;
    }

    public Dexception(Situation situation, List<KeyValuePair<string, string>> tokens, string? description = null, string? traceId = null,
        Dictionary<string, string>? payload = null)
    {
        var rendered = situation.Message;

        if (tokens is {Count: > 0})
            tokens.ForEach(token => rendered = rendered.Replace(token.Key, token.Value));

        Message = rendered;
        Key = situation.Key;
        Description = description;
        HttpCode = situation.ResultCode;
        SituationCode = situation.Status;
        Level = situation.Level;
        Payload = payload ?? new Dictionary<string, string>();
        TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId;
    }

    public Dexception(string message, string? key = null, string? description = null, HttpStatusCode httpCode = HttpStatusCode.InternalServerError,
        string level = "BLOCKER", string? traceId = null, int? situationCode = null, Dictionary<string, string>? payload = null)
    {
        Message = message;
        Key = key;
        Description = description;
        HttpCode = httpCode;
        SituationCode = situationCode;
        Level = level;
        Payload = payload ?? new Dictionary<string, string>();
        TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId;
    }

    public Dexception(string message, Dexception innerException, string? key = null, string? description = null,
        HttpStatusCode httpCode = HttpStatusCode.InternalServerError, string? traceId = null, int? situationCode = null,
        string level = "BLOCKER", Dictionary<string, string>? payload = null)
        : base(message, innerException)
    {
        Message = message;
        Key = key;
        Description = description;
        HttpCode = httpCode;
        SituationCode = situationCode;
        Level = level;
        Payload = payload ?? new Dictionary<string, string>();
        TraceId = string.IsNullOrWhiteSpace(traceId) ? Activity.Current?.Id ?? Guid.NewGuid().ToString() : traceId;
    }
}