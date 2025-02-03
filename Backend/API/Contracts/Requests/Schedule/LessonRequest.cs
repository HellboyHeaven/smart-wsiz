using Core.Models;

namespace API.Contracts.Requests.Schedule;


public class LessonRequest
{
    public Guid Id { get; set; }
    public LessonState State { get; set; } = LessonState.Class;
    public required string Room { get; set; }
    public required Guid Module { get; set; }
    public required DateOnly Date { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }

}