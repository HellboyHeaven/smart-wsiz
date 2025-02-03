using Core.Models;

namespace API.Contracts.Requests.Curriculum;


public record GroupRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid CourseId { get; set; }
    public required Language Language { get; set; }
    public List<Guid> StudentIds { get; set; } = new List<Guid>();
}