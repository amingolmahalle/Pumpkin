using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pumpkin.Utils;
using Pumpkin.Web.Filters.Validator.Dto;

namespace Pumpkin.Web.Filters.Validator
{
    public class ValidatorActionFilter :  ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new ErrorFluentValidation
                {
                    Errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList()
                })
                {
                    StatusCode = Constants.FluentValidationHttpStatusCode
                };
            }

        }
    }
}