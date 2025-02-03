using API.Contracts.Requests.Performance;
using API.Contracts.Responses.Performance;
using Core.Interfaces;
using Core.Models;
using Riok.Mapperly.Abstractions;

namespace API.Mappings;

[Mapper]
public partial class PerformanceMapper :
    IMapper<Attendance, AttendanceResponse, AttendanceRequest>,
    IMapper<Grade, GradeResponse, GradeRequest>
{
    public partial Attendance Map(AttendanceRequest request);

    [MapProperty([nameof(Attendance.Lesson), nameof(Attendance.Lesson.Date)], nameof(AttendanceResponse.Date))]
    public partial AttendanceResponse Map(Attendance entity);
    public partial IEnumerable<AttendanceResponse> Map(IEnumerable<Attendance> entities);

    public partial Grade Map(GradeRequest request);

   
    public partial GradeResponse Map(Grade entity);
    public partial IEnumerable<GradeResponse> Map(IEnumerable<Grade> entities);
}
