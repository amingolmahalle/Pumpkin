using Framework.Contracts.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pumpkin.Infrastructure.Framework.HttpResults.Contracts;

namespace Pumpkin.Infrastructure.Framework.HttpResults;

[DefaultStatusCode(DefaultStatusCode)]
public class StandardForcedPageResponse : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    public StandardForcedPageResponse(EmptyResponse response) : base(new StandardForcedResponseContract
    {
        Result = new StandardForcedResultContract
        {
            Level = response.Level,
            Title = response.Key,
            Message = response.Title,
            Description = response.Description,
            Status = response.SituationCode,
            TraceId = response.TraceId
        }
    })
        => StatusCode = (int) response.HttpCode;
}

[DefaultStatusCode(DefaultStatusCode)]
public class StandardForcedPageResponse<TData> : ObjectResult
{
    private const int DefaultStatusCode = StatusCodes.Status200OK;

    public StandardForcedPageResponse(DataResponse<PageableData<TData>> response) : base(new StandardResponseContract<TData>
    {
        Data = response.Data.Results,
        Paging = new Paging
        {
            Page = response.Data.Page,
            Size = response.Data.Size,
            Count = response.Data.Count,
            Pages = response.Data.Pages
        },
        Result = new StandardForcedResultContract
        {
            Level = response.Level,
            Title = response.Key,
            Message = response.Title,
            Description = response.Description,
            Status = response.SituationCode,
            TraceId = response.TraceId
        }
    })
        => StatusCode = (int) response.HttpCode;
}