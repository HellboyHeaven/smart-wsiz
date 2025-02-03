using API.Contracts.Requests.Performance;
using API.Contracts.Responses.Performance;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

[Authorize(Policy = "TeacherPolicy")]
public class AttendanceController : ApiControllerBase<Attendance, AttendanceResponse, AttendanceRequest, PerformanceMapper, Guid>
{

    public override async Task<IActionResult> Get([FromQuery] Guid ModuleId, [FromServices] GetAllEntitiesHandler<Attendance> action)
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var command = new GetAllQuery<Attendance>(
            filter:
                a => a.Lesson.Module.Id == ModuleId &
                a.Lesson.Module.Teacher.Id == userId,
            includes: q => q.Include(a => a.Lesson).ThenInclude(l => l.Module).Include(a => a.Student)
            );

        var response = await action.Handle(command);
        var result = Mapper.Map(response);
        return Ok(result);
    }
}
