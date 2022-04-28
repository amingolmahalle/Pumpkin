using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Pumpkin.Web.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseController : ControllerBase
{
    protected ILogger Logger { get; }

    protected IConfiguration Configuration { get; }
        
    protected bool UserIsAuthenticated => HttpContext.User.Identity is {IsAuthenticated: true};

    protected BaseController(IServiceProvider serviceProvider)
    {
        Configuration = serviceProvider.GetRequiredService<IConfiguration>();
        Logger = serviceProvider.GetRequiredService<ILogger<BaseController>>();
    }
}