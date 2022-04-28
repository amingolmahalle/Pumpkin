using Microsoft.AspNetCore.Mvc;
using Pumpkin.Web.Controller;
using SampleWebApi.Domain.Service.Commands.AddUser;
using SampleWebApi.Domain.Service.Commands.EditUser;
using SampleWebApi.Domain.Service.Queries.GetUserById;
using SampleWebApi.Domain.Service.Queries.GetUserByMobile;

namespace SampleWebApi.Web.Controllers;

[ApiVersion("1")]
public class UserController : BaseController
{
    private readonly IGetUserByIdService _getUserByIdService;

    private readonly IGetUserByMobileService _getUserByMobileService;

    private readonly IAddUserService _addUserService;

    private readonly IEditUserService _editUserService;

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
    }

    [HttpGet("GetUserById/{Id}")]
    public async Task<object> GetById(
        [FromRoute] GetUserByIdRequest request,
        CancellationToken cancellationToken)
    {
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