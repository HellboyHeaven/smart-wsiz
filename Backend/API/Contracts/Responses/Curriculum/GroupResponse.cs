using Core.Models;

namespace API.Contracts.Responses.Curriculum;


public record GroupResponse(Guid Id, string Name, Language Language, CourseResponse Course, string Code);
