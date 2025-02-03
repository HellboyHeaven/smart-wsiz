using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class BusStation
{
    [Key] public Guid Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;

    public List<BusTimetable> Timetables { get; set; } = new List<BusTimetable>();
}

