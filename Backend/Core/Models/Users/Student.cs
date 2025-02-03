using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Users;


[Index(nameof(StudentId), IsUnique = true)]
public class Student : User
{
    [Required] public string StudentId { get; set; } = string.Empty;
    [Required] public Course Course { get; set; }
    public Group? Group { get; set; }
    public List<Module> Modules { get; set; } = new List<Module>();
    public List<Subject> Subjects { get; set; } = new List<Subject>();
    public List<Grade> Grades { get; set; } = new List<Grade>();
    public List<Attendance> Attendances { get; set; } = new List<Attendance>();
}