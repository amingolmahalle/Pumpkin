using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Core.RequestWrapper;

namespace Pumpkin.Web.BaseClasses
{
    public class BaseApiController : ControllerBase
    {
        // protected ILog Logger = LogManager.GetLogger("ApiController");
        protected void ThrowExceptionIf(Func<bool> predicate, string message, params string[] errors)
        {
            if (predicate.Invoke())
            {
                // Logger<>.Error(message);
                throw new ApiException(message: message, errors: errors.Select(it => new ValidationError(null, it)));
            }
        }

        protected void ThrowExceptionIf(Func<Task<bool>> predicate, string message, params string[] errors)
        {
            if (predicate.Invoke().Result)
            {
                // Logger<>.Error(message);
                throw new ApiException(message: message, errors: errors.Select(it => new ValidationError(null, it)));
            }
        }
    }
}