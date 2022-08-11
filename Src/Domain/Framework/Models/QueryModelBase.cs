using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Domain.Framework.Models;

public class QueryModelBase : IQueryModel
{
    public ILog Logger { get; }

    public QueryModelBase(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<CommandModelBase>();
    }

}