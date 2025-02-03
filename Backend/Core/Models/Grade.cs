using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Core.Models;

[Index(nameof(Student), nameof(Subject), IsUnique = true)]
public class Grade
{
    [Key] public Guid Id { get; set; }
    [Required] public Subject? Subject { get; set; }
    [Required] public Student? Student { get; set; }
    [Required] public Module? Module { get; set; }
    public GradeTerm Term { get; set; }
    public string? Value { get; set; } = null;
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("grade_term")]
public enum GradeTerm
{
    FirstTerm,
    SecondTerm,
    Conditional,
    Advanced,
    Commission,
}