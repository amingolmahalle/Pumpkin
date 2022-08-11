using Domain.Framework.Logging;
using Framework.Contracts.Response;
using Infrastructure.Framework.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClientWebApi;

[ApiController]
[Produces("application/json")]
[Route("api/[area]")]
public class BaseController : ControllerBase
{
    public ILog Logger { get; }

    public BaseController(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<BaseController>();
    }

    #region .:: RESPONSE HELPERS ::.

    [NonAction]
    protected StandardForcedResponse Dynamic(EmptyResponse response) => new(response);

    [NonAction]
    protected StandardForcedResponse<TData> Dynamic<TData>(DataResponse<TData> response) => new(response);

    #endregion
}