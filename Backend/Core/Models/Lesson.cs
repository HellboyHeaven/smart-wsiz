using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Lesson
{
    [Key] public Guid Id { get; set; }
    [Required] public LessonState State { get; set; }
    [Required] public string Room { get; set; } = string.Empty;
    [Required] public Module? Module { get; set; }
    [Required] public DateOnly Date { get; set; } = new DateOnly();
    [Required] public TimeOnly StartTime { get; set; }
    [Required] public TimeOnly EndTime { get; set; }


}


[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("lesson_state")]
public enum LessonState
{
    Class,
    Exam,
    Canceled
}
