using Core.Models;

namespace API.Contracts.Responses.Curriculum;

public record SubjectResponse(Guid id, SubjectType Type, string Name, GradeType GradeType);