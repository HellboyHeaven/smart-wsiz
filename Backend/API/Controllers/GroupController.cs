using API.Contracts.Requests.Curriculum;
using API.Contracts.Responses.Curriculum;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Interfaces;
using Core.Models;
using Core.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GroupController(IRepository<Student> studentRepository, IRepository<Course> courseRepository)
    : ApiControllerBase<Group, GroupResponse, GroupRequest, CurriculumMapper>
{

    public override async Task<IActionResult> Add([FromBody] GroupRequest request, [FromServices] CreateEntityHandler<Group> action)
    {
        var entity = Mapper.Map(request);
        entity.Students = await studentRepository.GetByIdsAsync(request.StudentIds);
        entity.Course = await courseRepository.GetByIdAsync(request.CourseId);
        var command = new CreateCommand<Group>(entity);
        await action.Handle(command);
        return Ok("successful");
    }

    public override async Task<IActionResult> Update([FromBody] GroupRequest request, [FromServices] UpdateEntityHandler<Group> action)
    {
        var entity = Mapper.Map(request);
        entity.Students = await studentRepository.GetByIdsAsync(request.StudentIds);
        entity.Course = await courseRepository.GetByIdAsync(request.CourseId);
        var command = new UpdateCommand<Group>(entity);
        await action.Handle(command);
        return Ok("successful");
    }
}
