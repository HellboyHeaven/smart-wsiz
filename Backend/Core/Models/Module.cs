using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Module
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public Subject? Subject { get; set; }
    public Teacher? Teacher { get; set; }
    public IEnumerable<Group> Groups { get; set; } = new List<Group>();
    public IEnumerable<Student> Students { get; set; } = new List<Student>();
    public IEnumerable<Lesson> Lessons { get; set; } = new List<Lesson>();
    public IEnumerable<Attendance> Attendances { get; set; } = new List<Attendance>();
    public IEnumerable<Grade> Grades { get; set; } = new List<Grade>();

}