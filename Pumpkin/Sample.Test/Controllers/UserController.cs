using System;
using Pumpkin.Web.BaseClasses;

namespace Sample.Test.Controllers
{
    public class UserController : BaseController
    {
        protected UserController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}