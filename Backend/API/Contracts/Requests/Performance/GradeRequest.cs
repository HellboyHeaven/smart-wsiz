using Core.Models;

namespace API.Contracts.Requests.Performance;


public record GradeRequest
{
    public required Guid Id { get; set; }
    public GradeTerm Term = GradeTerm.FirstTerm;
    public required string? Value { get; set; }
}