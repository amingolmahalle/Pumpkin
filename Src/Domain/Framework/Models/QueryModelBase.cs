using Domain.Framework.Logging;
using Microsoft.AspNetCore.Http;

namespace Domain.Framework.Models;

public class QueryModelBase : IQueryModel
{
    public ILog Logger { get; }

    public QueryModelBase(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<CommandModelBase>();
    }

}