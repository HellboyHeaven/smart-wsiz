namespace API.Contracts.Requests.Curriculum;


public record CourseRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
}