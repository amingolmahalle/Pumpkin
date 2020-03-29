using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Web.BaseClasses;

namespace Sample.Test.Controllers
{
    [Route("value")]
    [Produces("application/json")]
    public class ValueController : BaseApiController
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string>
            {
                "hello", "flowers", "in", "home"
            };
        }
    }
}