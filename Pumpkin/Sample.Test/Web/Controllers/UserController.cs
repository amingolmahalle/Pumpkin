using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Contract.Logging;
using Pumpkin.Web.Controller;
using Sample.Test.Domain.Service.Commands.AddUser;
using Sample.Test.Domain.Service.Commands.EditUser;
using Sample.Test.Domain.Service.Queries.GetUserById;
using Sample.Test.Domain.Service.Queries.GetUserByMobile;

namespace Sample.Test.Web.Controllers
{
    [ApiVersion("1")]
    public class UserController : BaseController
    {
        private readonly IGetUserByIdService _getUserByIdService;

        private readonly IGetUserByMobileService _getUserByMobileService;

        private readonly IAddUserService _addUserService;

        private readonly IEditUserService _editUserService;

        private static ILog _logger;

        public UserController(
            IServiceProvider serviceProvider,
            IGetUserByIdService getUserByIdService,
            IAddUserService addUserService,
            IEditUserService editUserService,
            IGetUserByMobileService getUserByMobileService) : base(serviceProvider)
        {
            _getUserByIdService = getUserByIdService;
            _addUserService = addUserService;
            _editUserService = editUserService;
            _getUserByMobileService = getUserByMobileService;
            
            _logger = LogManager.GetLogger<UserController>();
        }

        [HttpGet("GetUserById/{Id}")]
        public async Task<GetUserByIdResponse> GetById([FromRoute] GetUserByIdRequest request,
            CancellationToken cancellationToken)
        {
            _logger.Info("info log in user controller for test log");
            return await _getUserByIdService.ExecuteAsync(request, cancellationToken);
        }

        [HttpGet("GetUserByMobile/{MobileNumber}")]
        public async Task<GetUserByMobileResponse> GetByMobile([FromRoute] GetUserByMobileRequest request,
            CancellationToken cancellationToken)
        {
            return await _getUserByMobileService.ExecuteAsync(request, cancellationToken);
        }

        [HttpPost("AddUser")]
        public async Task Add([FromBody] AddUserRequest request, CancellationToken cancellationToken)
        {
            await _addUserService.ExecuteAsync(request, cancellationToken);
        }

        [HttpPut("EditUser")]
        public async Task Edit([FromBody] EditUserRequest request, CancellationToken cancellationToken)
        {
            await _editUserService.ExecuteAsync(request, cancellationToken);
        }
    }
}