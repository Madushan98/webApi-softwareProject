using AuthService.Contract;
using AuthService.Domain.Filters;
using AuthService.Models.Request.Queries;
using AuthService.Services;
using AutoMapper;
using DTOLibrary.Common;
using DTOLibrary.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

public class UserController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserService _service;

    public UserController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost(ApiRoutes.User.Create)]
    [ProducesResponseType(typeof(UserResponse), 200)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest createUserRequest)
    {
        var response = await _service.CreateUserAsync(createUserRequest);
        if (response == null) return BadRequest();

        return Accepted(response);
    }

    [HttpGet(ApiRoutes.User.GetAll)]
    public async Task<IActionResult> GetAllAsync([FromQuery] GetAllUserQuery query,
        [FromQuery] PaginationRequest paginationRequest)
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(paginationRequest);
        var filter = _mapper.Map<UserFilter>(query);
        var resonse = await _service.GetAllUsersAsync(filter, paginationFilter);
        if (resonse == null) return BadRequest();

        return Accepted(resonse);
    }
}