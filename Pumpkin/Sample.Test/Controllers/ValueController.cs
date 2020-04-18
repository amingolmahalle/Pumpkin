using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pumpkin.Web.BaseClasses;

namespace Sample.Test.Controllers
{
    [Produces("application/json")]
    public class ValueController : BaseController
    {
        private readonly IConfiguration _configuration;

        public ValueController(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider)
        {
            _configuration = configuration;
        }

        [HttpGet("get")]
        public List<string> Get()
        {
            return new List<string>
            {
                "hello",
                "flowers",
                "in",
                "home",
                _configuration.GetConnectionString("DefaultConnectionString") ?? "i am empty"
            };
        }
    }
}