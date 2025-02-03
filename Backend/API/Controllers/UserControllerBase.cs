using API.Contracts.Requests.Auth;
using API.Contracts.Responses.Auth;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Interfaces;
using Core.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserControllerBase<TUser, TResponse, TRequest> : ControllerBase
    where TUser : User, new()
    where TResponse : UserResponse
    where TRequest : UserRequest
{
    protected readonly IMapper<User, UserResponse, UserRequest> Mapper = new UserMapper();

    [HttpGet]
    public virtual async Task<IActionResult> Get([FromServices] GetAllEntitiesHandler<TUser> getAll)
    {
        var command = new GetAllQuery<TUser>();
        var response = await getAll.Handle(command);
        return Ok(Mapper.Map(response));
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<TUser> action)
    {
        var command = new GetByIdQuery<TUser>(id);
        var response = await action.Handle(command);
        return Ok(Mapper.Map(response));
    }



    [HttpPut]
    public virtual async Task<IActionResult> Get(TRequest request, [FromServices] UpdateEntityHandler<User> action)
    {
        var command = new UpdateCommand<User>(Mapper.Map(request));
        await action.Handle(command);
        return Ok("success");
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Get(Guid id, [FromServices] DeleteEntityByIdHandler<TUser> action)
    {
        var command = new DeleteByIdCommand<TUser>(id);
        await action.Handle(command);
        return Ok("success");
    }
}
