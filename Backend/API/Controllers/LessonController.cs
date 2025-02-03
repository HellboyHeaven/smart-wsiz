using API.Contracts.Requests.Schedule;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Application.Handlers.Lesson;
using Core.Enums;
using Core.Extensions;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.Controllers;


public class LessonController(CreateLesson createLesson, GetLessons getLessons) : ApiControllerBase<Lesson, LessonResponse, LessonRequest, ScheduleMapper, DateOnly?>
{
    [Authorize(Policy = "AdminPolicy")]
    public override async Task<IActionResult> Add([FromBody] LessonRequest request, [FromServices] CreateEntityHandler<Lesson> action)
    {
        var command = new CreateLessonCommand(Mapper.Map(request), request.Module);
        await createLesson.Handle(command);
        return Ok("success");
    }


    [Authorize(Policy = "AuthorizePolicy")]
    public override async Task<IActionResult> Get(DateOnly? date, [FromServices] GetAllEntitiesHandler<Lesson> action)
    {
        var command = new GetLessonQuery(User, date);
        var response = await getLessons.Handle(command);
        return Ok(Mapper.Map(response));
    }
}
