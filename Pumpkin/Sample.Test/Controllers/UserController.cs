using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pumpkin.Web.Controller;
using Sample.Test.Domain.Service.Commands.AddUser;
using Sample.Test.Domain.Service.Queries.GetUserById;

namespace Sample.Test.Controllers
{
    public class UserController : BaseController
    {
        private readonly IGetUserByIdService _getUserByIdService;

        private readonly IAddUserService _addUserService;

        public UserController(
            IServiceProvider serviceProvider,
            IGetUserByIdService getUserByIdService,
            IAddUserService addUserService) : base(serviceProvider)
        {
            _getUserByIdService = getUserByIdService;
            _addUserService = addUserService;
        }

        [HttpGet("GetUserById")]
        public async Task<GetUserByIdResponse> GetById(GetUserByIdRequest request,CancellationToken cancellationToken)
        {
            return await _getUserByIdService.ExecuteAsync(request, cancellationToken);
        }

        [HttpPost("AddUser")]
        public async Task Add([FromBody] AddUserRequest request, CancellationToken cancellationToken)
        {
            await _addUserService.ExecuteAsync(request, cancellationToken);
        }

        // [HttpGet("EditUser")]
        // public async Task<GetUserByIdResponse> Edit(GetUserByIdRequest request)
        // {
        //     return await _getUserByIdService.ExecuteAsync(request);
        // }
    }
}