using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Contract.Caching;
using Pumpkin.Web.Controller;

namespace Sample.Test.Web.Controllers
{
    [ApiVersion("1")]
    public class RedisController : BaseController
    {
        private readonly ICacheService _cacheService;

        public RedisController(
            IServiceProvider serviceProvider,
            ICacheService cacheService) : base(serviceProvider)
        {
            _cacheService = cacheService;
        }

        [HttpGet("SendOtp")]
        public async Task<string> SendOtp()
        {
            await _cacheService.SetAsync("otp",
                "test",
                "your code is: 1234",
                DateTime.Now.AddMinutes(5),
                new CacheOptions(CacheProviderType.Shared));
            
           return await _cacheService.GetAsync<string>("otp",
                "test",
                new CacheOptions(CacheProviderType.Shared));
        }
    }
}