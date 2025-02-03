using API.Contracts.Responses.Auth;
using API.Contracts.Responses.Curriculum;


namespace API.Contracts.Responses.Schedule;

public record ModuleResponse(Guid Id, string Name, SubjectResponse Subject, TeacherResponse Teacher, List<string> Groups);




//public record ModuleDetailResponse(Guid Id, string Name, SubjectResponse Subject, List<string> Groups, );

