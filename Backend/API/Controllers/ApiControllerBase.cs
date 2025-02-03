
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase<TEntity, TResponse, TRequest, TMapper, TFilter> : Controller
    where TEntity : class, new()
    where TMapper : class, IMapper<TEntity, TResponse, TRequest>, new()
    where TFilter : new()
{
    protected readonly IMapper<TEntity, TResponse, TRequest> Mapper = new TMapper();


    [HttpGet]
    public virtual async Task<IActionResult> Get([FromQuery] TFilter filter, [FromServices] GetAllEntitiesHandler<TEntity> action)
    {
        var command = new GetAllQuery<TEntity>();
        var response = await action.Handle(command);
        var result = Mapper.Map(response);

        return Ok(result);
    }




    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<TEntity> action)
    {
        var command = new GetByIdQuery<TEntity>(Id: id);
        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add([FromBody] TRequest request, [FromServices] CreateEntityHandler<TEntity> action)
    {
        var command = new CreateCommand<TEntity>(Mapper.Map(request));
        await action.Handle(command);
        return Ok("successful");
    }

    [HttpPut]
    public virtual async Task<IActionResult> Update([FromBody] TRequest request, [FromServices] UpdateEntityHandler<TEntity> action)
    {
        var command = new UpdateCommand<TEntity>(Mapper.Map(request));
        await action.Handle(command);
        return Ok("successful");
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Delete(Guid id, [FromServices] DeleteEntityByIdHandler<TEntity> action)
    {
        var command = new DeleteByIdCommand<TEntity>(id);
        await action.Handle(command);
        return Ok("success");
    }
}

[Route("api/[controller]")]
[ApiController]
public abstract class ApiControllerBase<TEntity, TResponse, TRequest, TMapper> : Controller
    where TEntity : class, new()
    where TMapper : class, IMapper<TEntity, TResponse, TRequest>, new()
{
    protected readonly IMapper<TEntity, TResponse, TRequest> Mapper = new TMapper();



    [HttpGet]
    public virtual async Task<IActionResult> Get([FromServices] GetAllEntitiesHandler<TEntity> action)
    {
        var command = new GetAllQuery<TEntity>();
        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }




    [HttpGet("{id}")]
    public virtual async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<TEntity> action)
    {
        var command = new GetByIdQuery<TEntity>(Id: id);
        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Add([FromBody] TRequest request, [FromServices] CreateEntityHandler<TEntity> action)
    {
        var command = new CreateCommand<TEntity>(Mapper.Map(request));
        await action.Handle(command);
        return Ok("successful");
    }

    [HttpPut]
    public virtual async Task<IActionResult> Update([FromBody] TRequest request, [FromServices] UpdateEntityHandler<TEntity> action)
    {
        var command = new UpdateCommand<TEntity>(Mapper.Map(request));
        await action.Handle(command);
        return Ok("successful");
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<IActionResult> Delete(Guid id, [FromServices] DeleteEntityByIdHandler<TEntity> action)
    {
        var command = new DeleteByIdCommand<TEntity>(id);
        await action.Handle(command);
        return Ok("success");
    }
}

