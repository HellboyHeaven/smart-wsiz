using Application.Handlers.Abstractions;
using Core.Enums;
using Core.Interfaces;
using Core.Models.Users;
using Core.Models;

using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using System;
using Core.Extensions;
using Application.Attributes;

namespace Application.Handlers.Lesson;

using Lesson = Core.Models.Lesson;

public record GetLessonQuery(ClaimsPrincipal User, DateOnly? Date) : IQuery<IEnumerable<Lesson>>;

[Service]
public class GetLessons(IRepository<Lesson> lessonRepository, IRepository<Course> courseRepository) : IQueryHandler<GetLessonQuery, IEnumerable<Lesson>>
{
    public async Task<IEnumerable<Lesson>> Handle(GetLessonQuery query, CancellationToken cancellationToken = default)
    {
        var role = query.User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();
        var userId = new Guid(query.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var _date = query.Date ?? DateOnly.FromDateTime(DateTime.Today);
        int week = (int)(_date.DayOfWeek + 6) % 7;
        var monday = _date.AddDays(-week);
        var sunday = _date.AddDays(6 - week);

        if (role == UserRole.Admin)
        {
            var result = await lessonRepository.GetAllAsync(l => l.Date == query.Date);
            return result;
        }

        if (role == UserRole.Teacher)
        {
            return await lessonRepository.GetAllAsync(
                filter: l => l.Module.Teacher.Id == userId &&
                l.Date >= monday && l.Date <= sunday);

        }

        if (role == UserRole.Student)
        {
            return await lessonRepository.GetAllAsync(
                filter: l => l.Module.Students.Any(s => s.Id == userId) &&
                l.Date >= monday && l.Date <= sunday);
        }

        return [];
    }
}



