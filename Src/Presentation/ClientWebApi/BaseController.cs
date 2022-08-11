using Framework.Contracts.Response;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Domain.Framework.Logging;
using Pumpkin.Infrastructure.Framework.HttpResults;

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