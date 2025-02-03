using API.Contracts.Responses.Auth;

namespace API.Contracts.Responses.Performance;


public record AttendanceStudentResponse(Guid id, string Firstname, string Lastname, string StudentId);

public record AttendanceResponse(Guid Id, DateOnly Date, bool IsPresent, AttendanceStudentResponse Student);
