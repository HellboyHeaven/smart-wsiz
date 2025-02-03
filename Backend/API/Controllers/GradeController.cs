using API.Contracts.Requests.Performance;
using API.Contracts.Responses.Performance;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers;

public class GradeController(IRepository<Grade> gradeRepository)
    : ApiControllerBase<Grade, GradeResponse, GradeRequest, PerformanceMapper, Guid?>()
{

    public override async Task<IActionResult> Get([FromQuery] Guid? moduleId, [FromServices] GetAllEntitiesHandler<Grade> action)
    {
        var role = User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (role == UserRole.Student && moduleId == null)
        {
            var command = new GetAllQuery<Grade>(
                filter: g => g.Student.Id == userId,
               includes: q => q.Include(m => m.Student).Include(g => g.Subject));
            var result = await action.Handle(command);
            return Ok(Mapper.Map(result));
        }
        if (role == UserRole.Teacher && moduleId != null)
        {
            
            var grades = await gradeRepository.GetAllAsync(
                filter: g => g.Module.Id == moduleId.Value && g.Module.Teacher.Id == userId,
                includes: q => q.Include(g => g.Module).Include(g => g.Student));
            return Ok(Mapper.Map(grades));
        }

        return BadRequest("INvalid role and params");
    }


    [Authorize(Policy = "TeacherPolicy")]
    public override async Task<IActionResult> Update([FromBody] GradeRequest request, [FromServices] UpdateEntityHandler<Grade> action)
    {
        var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var grade = await gradeRepository.GetByIdAsync(request.Id, includes: q => q.Include(g => g.Module).ThenInclude(m => m.Teacher));
        if (grade?.Module?.Teacher?.Id != userId)
        {
            return BadRequest();
        }

        grade.Term = request.Term;
        grade.Value = request.Value;
        var command = new UpdateCommand<Grade>(grade);
        await action.Handle(command);
        return Ok("successful");
    }
}
