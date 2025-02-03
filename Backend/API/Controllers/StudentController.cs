using API.Contracts.Requests.Auth;
using API.Contracts.Responses.Auth;
using Application.Exceptions;
using Application.Handlers.Abstractions;
using Application.Handlers.Auth;
using Application.Handlers.Common;
using Core.Interfaces;
using Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class StudentController : UserControllerBase<Student, StudentResponse, StudentRequest>
{
    [Authorize(Policy = "AdminPolicy")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] StudentRequest registerRequest, [FromServices] Register registerHandler)
    {
        try
        {

            await registerHandler.Handle(new RegisterStudentCommand(Mapper.Map(registerRequest) as Student, registerRequest.Course));
            return Ok("success");
        }
        catch (AlreadyExistsException e)
        {
            return ValidationProblem(e.Message);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpGet]
    public override async Task<IActionResult> Get([FromServices] GetAllEntitiesHandler<Student> getAll)
    {
        var command = new GetAllQuery<Student>(includes: q => q.Include(s => s.Course));
        var response = await getAll.Handle(command);
        return Ok(Mapper.Map(response));
    }

    [HttpGet("{id:guid}")]
    public override async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<Student> action)
    {
        var command = new GetByIdQuery<Student>(id, includes: q => q.Include(s => s.Course));
        var response = await action.Handle(command);
        return Ok(Mapper.Map(response));
    }

}
