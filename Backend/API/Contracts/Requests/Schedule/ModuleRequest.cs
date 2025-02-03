namespace API.Contracts.Requests.Schedule;


public record ModuleRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid SubjectId { get; set; }
    public Guid? TeacherId { get; set; }
    public List<Guid> StudentIds { get; set; } = new List<Guid>();
}