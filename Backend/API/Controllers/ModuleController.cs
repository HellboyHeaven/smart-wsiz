using API.Contracts.Requests.Schedule;
using API.Contracts.Responses.Schedule;
using API.Mappings;
using Application.Handlers.Abstractions;
using Application.Handlers.Common;
using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Core.Models;
using Core.Models.Users;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace API.Controllers;


public class ModuleController(
    IRepository<User> userRepository,
    IRepository<Student> studentRepository,
    IRepository<Teacher> teacherRepository,
    IRepository<Subject> subjectRepository,
    IRepository<Grade> gradeRepository)
    : ApiControllerBase<Module, ModuleResponse, ModuleRequest,ScheduleMapper, Guid?>
{

    public override async Task<IActionResult> Add([FromBody] ModuleRequest request, [FromServices] CreateEntityHandler<Module> action)
    {
        var entity = Mapper.Map(request);

        
        var students = await studentRepository.GetByIdsAsync(request.StudentIds);
        var teacher = await teacherRepository.GetByIdAsync(request.TeacherId.Value);
        var subject = await subjectRepository.GetByIdAsync(request.SubjectId);


        entity.Students = students;
        entity.Teacher = teacher;
        entity.Subject = subject;
        Console.WriteLine(students.Count());
        var command = new CreateCommand<Module>(entity);
        

        List<Grade> grades = new ();
        Console.WriteLine(students.Count());

        foreach (var student in students)
        {
            Console.WriteLine(student.Firstname);
            foreach (GradeTerm term in (GradeTerm[])Enum.GetValues(typeof(GradeTerm)))
            {
                Console.WriteLine(term);
                var grade = new Grade();
                grade.Subject = subject;
                grade.Student = student;
                grade.Module = entity;
                grade.Term = term;
                grade.Value = null;
                grades.Add(grade);
            }
        }
      

        await action.Handle(command);
        await gradeRepository.AddAllAsync(grades);
       

        return Ok("successful");
    }

    public override async Task<IActionResult> Update([FromBody] ModuleRequest request, [FromServices] UpdateEntityHandler<Module> action)
    {
        var entity = Mapper.Map(request);

        entity.Students = await studentRepository.GetByIdsAsync(request.StudentIds);
        entity.Teacher = request.TeacherId == null ? null : await teacherRepository.GetByIdAsync(request.TeacherId.Value);

        var command = new UpdateCommand<Module>(entity);
        await action.Handle(command);
        return Ok("successful");
    }

    //[OverrideAuthorization]
    [Authorize(Policy = "TeacherPolicy")]
    public override async Task<IActionResult> Get(Guid? ownerId, [FromServices] GetAllEntitiesHandler<Module> action)
    {
        var role = User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();


        if (role == UserRole.Admin && ownerId == null)
        {
            var command = new GetAllQuery<Module>(
               includes: q => q.Include(m => m.Teacher).Include(m => m.Subject).Include(m => m.Groups));
            var result = await action.Handle(command);
            return Ok(Mapper.Map(result));
        }
        
        
        if (role == UserRole.Admin && ownerId != null)
        {
            var user = await userRepository.GetByIdAsync(ownerId.Value);

            if (user is Student student)
            {
                var command = new GetAllQuery<Module>(
                    includes: q => q.Include(m => m.Teacher).Include(m => m.Subject).Include(m => m.Groups),
                    filter: m => m.Students.Contains(student));
                var result = await action.Handle(command);
                return Ok(Mapper.Map(result));
            }

            if (user is Teacher teacher)
            {
                var command = new GetAllQuery<Module>(
                    includes: q => q.Include(m => m.Teacher).Include(m => m.Subject).Include(m => m.Groups),
                    filter: m => m.Teacher == teacher);
                var result = await action.Handle(command);
                return Ok(Mapper.Map(result));
            }


        }


        if (role == UserRole.Teacher)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var command = new GetAllQuery<Module>(
                filter: c => c.Teacher.Id == new Guid(userId),
                includes: q => q.Include(m => m.Teacher).Include(m => m.Subject).Include(m => m.Groups)
                );
            var result = await action.Handle(command);
            return Ok(Mapper.Map(result));
        }

        return BadRequest();
    }

    public override async Task<IActionResult> Get(Guid id, [FromServices] GetEntityByIdHandler<Module> action)
    {
        var role = User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();
    

        if (role == UserRole.Teacher)
        {

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var command = new GetByIdQuery<Module>(
                id,
                includes: q => q.Include(m => m.Teacher).Include(m => m.Subject).Include(m => m.Groups)
                );
            
            var result = await action.Handle(command);

            if (result.Teacher.Id != userId)
            {
                return BadRequest();
            }
            return Ok(Mapper.Map(result));
        }

        return BadRequest();
    }


}
