using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

[Index(nameof(Name), nameof(Code), IsUnique = true)]
public class Course
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Code { get; set; } = string.Empty;
    public IEnumerable<Student> Students { get; set; } = new List<Student>();
}
