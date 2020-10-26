using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Contract.Caching;
using Pumpkin.Contract.Logging;
using Pumpkin.Web.Controller;
using Helpers = Pumpkin.Common.Helpers.Helpers;

namespace Sample.Test.Web.Controllers
{
    [ApiVersion("1")]
    public class RedisController : BaseController
    {
        private readonly ICacheService _cacheService;

        private readonly ILog _logger;

        public RedisController(
            IServiceProvider serviceProvider,
            ICacheService cacheService) : base(serviceProvider)
        {
            _cacheService = cacheService;
            _logger = LogManager.GetLogger<UserController>();
        }

        [HttpGet("SendOtp/{mobileNumber}")]
        public async Task SendOtp([FromRoute] string mobileNumber)
        {
            var code = Helpers.RandomRange(1111, 9999, 4);
            await _cacheService.SetAsync($"otp::${mobileNumber}",
                "test",
                code,
                DateTime.Now.AddMinutes(5),
                new CacheOptions(CacheProviderType.Shared));

            _logger.Info($"send otp successfully:{code}");
        }
    }
}