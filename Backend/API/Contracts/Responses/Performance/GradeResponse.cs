using API.Contracts.Responses.Auth;
using API.Contracts.Responses.Curriculum;
using Core.Models;

namespace API.Contracts.Responses.Performance;

public record GradeResponse(Guid Id, AttendanceStudentResponse Student, GradeTerm Term, string? Value, SubjectResponse Subject);
