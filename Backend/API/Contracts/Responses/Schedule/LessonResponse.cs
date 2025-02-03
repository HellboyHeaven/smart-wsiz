using API.Contracts.Responses.Auth;
using API.Contracts.Responses.Curriculum;
using API.Contracts.Responses.Schedule;
using Core.Models;

public record LessonResponse
{
    public SubjectType Type { get; set; }
    public LessonState State { get; set; }
    public DateOnly Date {  get; set; }
    public string Room { get; set; } = string.Empty;
    public TeacherResponse Teacher { get; set; }
    public ModuleResponse Module { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string[] Groups { get; set; } = [];
}
