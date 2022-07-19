using System.Net;

namespace Framework.Exceptions;

public class Situation
{
    public int Status { get; set; }
    public string Key { get; set; }
    public string Message { get; set; }
    public string Level { get; set; }
    public HttpStatusCode ResultCode { get; set; }

    public Situation(int status, string key, string message, string level = "BLOCKER", HttpStatusCode resultCode = HttpStatusCode.UnprocessableEntity)
    {
        Status = status;
        Key = key;
        Message = message;
        Level = level;
        ResultCode = resultCode;
    }

    public static Situation Make(string sitKey, int? status = null, string key = null, string message = null)
    {
        var situation = new Situation(status ?? 16200, key ?? "UNPROCESSABLE", message ?? ":پیام:");
        switch (sitKey)
        {
            case SitKeys.Unauthorized:
                situation =  new Situation(status ?? 16201, key ?? "UNAUTHORIZED", message ?? ":پیام:", resultCode: HttpStatusCode.Unauthorized);
                break;
            case SitKeys.Forbidden:
                situation =  new Situation(status ?? 16202, key ?? "FORBIDDEN", message ?? ":پیام:", resultCode: HttpStatusCode.Forbidden);
                break;
            case SitKeys.BadRequest:
                situation =  new Situation(status ?? 16203, key ?? "BAD_REQUEST", message ?? ":پیام:", resultCode: HttpStatusCode.BadRequest);
                break;
            case SitKeys.InvalidObject:
                situation =  new Situation(status ?? 16204, key ?? "INVALID_OBJECT", message ?? ":موجودیت: نامعتبر است.");
                break;
            case SitKeys.NotAllowed:
                situation =  new Situation(status ?? 16205, key ?? "NOT_ALLOWED", message ?? "امکان :عملیات: :موجودیت: :شرایط: وجود ندارد.");
                break;
            case SitKeys.NotFound:
                situation =  new Situation(status ?? 16206, key ?? "NOT_FOUND", message ?? "هیچ :موجودیت: :شرایط: پیدا نشد!");
                break;
            case SitKeys.AlreadyExists:
                situation =  new Situation(status ?? 16207, key ?? "ALREADY_EXISTS", message ?? ":موجودیت: دیگری با :شرایط: وجود دارد!");
                break;
        }

        if (status is <= 16220 or >= 16500)
            throw new Dexception("کد خطای خروجی در بازه معتبر نیست.", new Dexception(situation), situationCode: 16200);

        return situation;
    }
}