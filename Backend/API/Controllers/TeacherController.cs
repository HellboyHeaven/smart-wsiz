using API.Contracts.Requests.Auth;
using API.Contracts.Responses.Auth;
using Application.Exceptions;
using Application.Handlers.Auth;
using Core.Interfaces;
using Core.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TeacherController : UserControllerBase<Teacher, TeacherResponse, TeacherRequest>
{
    [Authorize(Policy = "AdminPolicy")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] TeacherRequest registerRequest, [FromServices] Register registerHandler)
    {
        try
        {
            await registerHandler.Handle(new RegisterTeacherCommand(Mapper.Map(registerRequest) as Teacher));
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
}
