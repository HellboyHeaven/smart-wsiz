using Application.Attributes;
using Application.Handlers.Auth;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;


namespace Application.Handlers.Lesson;

using Lesson = Core.Models.Lesson;
using Grade = Core.Models.Grade;

public record CreateLessonCommand(Lesson Lesson, Guid ModuleId) : ICommand;

[Service]
public class CreateLesson(IRepository<Lesson> lessonRepository, IRepository<Module> moduleRepository, IRepository<Attendance> attendanceRepository) : ICommandHandler<CreateLessonCommand>
{
    public async Task Handle(CreateLessonCommand command, CancellationToken cancellationToken = default)
    {
        var lesson = command.Lesson;
      
        var module = await moduleRepository.GetByIdAsync(command.ModuleId, includes: q => q.Include(m => m.Students));
        lesson.Module = module;
        lesson = await lessonRepository.AddAsync(lesson);

        Console.WriteLine(module.Students.Count());

        IEnumerable<Attendance> attendances = module.Students.Select(s =>
        {
            Console.WriteLine(s.Firstname);
            var attendance = new Attendance();
            attendance.Student = s;
            attendance.Lesson = lesson;
            attendance.IsPresent = false;
            return attendance;
        });
        Console.WriteLine(attendances.Count());

        await attendanceRepository.AddAllAsync(attendances);
    }
}

