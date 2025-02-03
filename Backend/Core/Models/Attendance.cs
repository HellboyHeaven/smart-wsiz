using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

[Index(nameof(Student), nameof(Lesson), IsUnique = true)]
public class Attendance
{
    [Key] public Guid Id { get; set; }
    [Required] public Student? Student { get; set; }
    [Required] public Lesson? Lesson { get; set; }
    [Required] public bool IsPresent { get; set; } = false;
}