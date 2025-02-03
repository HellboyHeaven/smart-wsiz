namespace API.Contracts.Requests.Auth;

public class StudentRequest : UserRequest
{
    required public string StudentId { get; set; } = string.Empty;
    required public Guid Course { get; set; }
}