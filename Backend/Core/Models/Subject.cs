using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Core.Models;

[Index(nameof(Name), nameof(Type), IsUnique = true)]
public class Subject
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public SubjectType Type { get; set; }
    [Required] public GradeType GradeType { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    public List<Module> Modules { get; set; } = new List<Module>();
    public List<Grade> Grades { get; set; } = new List<Grade>();
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("grade_type")]
public enum GradeType
{
    Graded,
    NonGraded
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("subject_type")]
public enum SubjectType
{
    Lecture,
    Laboratory,
    Workshop,
}
