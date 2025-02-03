using API.Contracts.Responses.Curriculum;

namespace API.Contracts.Responses.Auth;
public class StudentResponse : UserResponse
{
    public string StudentId { get; set; } = string.Empty;
    public CourseResponse Course { get; set; }

}
