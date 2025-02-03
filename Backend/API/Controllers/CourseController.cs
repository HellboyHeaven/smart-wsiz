using API.Contracts.Requests.Curriculum;
using API.Contracts.Responses.Curriculum;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Enums;
using Core.Extensions;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;


[Authorize(Policy = "AdminPolicy")]
public class CourseController : ApiControllerBase<Course, CourseResponse, CourseRequest, CurriculumMapper>
{

    [Authorize(Policy = "StudentPolicy")]
    public override async Task<IActionResult> Get([FromServices] GetAllEntitiesHandler<Course> action)
    {

        var role = User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();


        if (role == UserRole.Admin)
        {
            var adminCommand = new GetAllQuery<Course>();
            var adminResult = await action.Handle(adminCommand);
            return Ok(Mapper.Map(adminResult));
        }
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        var studentCommand = new GetAllQuery<Course>(filter: c => c.Students.Any(s => s.Id == new Guid(userId)));
        var studentResult = await action.Handle(studentCommand);

        return Ok(Mapper.Map(studentResult));

    }
}


