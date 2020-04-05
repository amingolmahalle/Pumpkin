using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Test.Controllers
{
    [Produces("application/json")]
    public class ValueController : ControllerBase
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