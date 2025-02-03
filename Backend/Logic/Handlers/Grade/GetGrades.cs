using Application.Attributes;
using Core.Enums;
using Core.Extensions;
using Core.Interfaces;
using Persistance.Repositories;
using System.Security.Claims;


namespace Application.Handlers.Grade;

using Grade = Core.Models.Grade;

public record GetGradeQuery(ClaimsPrincipal User, Guid? moduleId) : IQuery<IEnumerable<Grade>>;

[Service]
public class GetGrades(IRepository<Grade> gradeRepository) : IQueryHandler<GetGradeQuery, IEnumerable<Grade>>
{
    public async Task<IEnumerable<Grade>> Handle(GetGradeQuery query, CancellationToken cancellationToken = default)
    {
        var role = query.User.FindFirst(ClaimTypes.Role).Value.ParseEnum<UserRole>();
        var userId = new Guid(query.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        if (role == UserRole.Teacher)
        {
            return await gradeRepository.GetAllAsync(filter: g => g.Module.Id == query.moduleId && g.Module.Teacher.Id == userId);
        }

        if (role == UserRole.Student)
        {
            return await gradeRepository.GetAllAsync(filter: g => g.Module.Students.Any(s => s.Id == userId));
        }

        return [];
    }
}

