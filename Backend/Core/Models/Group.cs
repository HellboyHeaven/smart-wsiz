using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Group
{

    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public Language Language { get; set; }
    [Required] public Course Course { get; set; }
    public IEnumerable<Student> Students { get; set; } = new List<Student>();

    public override string ToString()
    {
        var lang = Language == Language.Polish ? 'P' : 'A';
        return $"{Name}-{lang}-{Course.Code}";
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("language")]
public enum Language
{
    English,
    Polish
}