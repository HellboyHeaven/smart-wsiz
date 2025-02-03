using Core.Models;

namespace API.Contracts.Requests.Curriculum;


public record SubjectRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required GradeType GradeType { get; init; }
    public required SubjectType Type { get; init; }
}