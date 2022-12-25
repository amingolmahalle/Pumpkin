using Microsoft.AspNetCore.Http;
using Pumpkin.Domain.Framework.Logging;

namespace Pumpkin.Application.Queries;

public abstract class QueriesBase
{
    public ILog Logger { get; }

    public QueriesBase(IHttpContextAccessor accessor)
    {
        Logger = LogManager.GetLogger<QueriesBase>();
    }
}