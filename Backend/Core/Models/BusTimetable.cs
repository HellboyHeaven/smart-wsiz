using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models;

[Index(nameof(Date), nameof(Route), nameof(Direction), nameof(Station), IsUnique = true)]
public class BusTimetable
{
    [Key] public Guid Id { get; set; }
    [Required] public uint Route;
    [Required] public BusDirection Direction { get; set; }
    [Required] public BusStation Station { get; set; }
    [Required] public DateOnly Date { get; set; }
    [Required] public TimeOnly Time { get; set; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
[Enum("bus_direction")]
public enum BusDirection
{
    To,
    From
}
