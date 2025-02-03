namespace API.Contracts.Requests.Performance;

public class AttendanceRequest
{
    public Guid Id { get; set; }
    public required bool IsPresent { get; set; }
}