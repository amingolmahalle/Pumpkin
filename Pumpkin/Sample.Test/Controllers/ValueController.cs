using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Web.BaseClasses;

namespace Sample.Test.Controllers
{
    [Produces("application/json")]
    public class ValueController : BaseController
    {
        protected ValueController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

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