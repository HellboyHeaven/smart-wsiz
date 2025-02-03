using API.Contracts.Requests.Schedule;
using API.Contracts.Responses.Schedule;
using Core.Interfaces;
using Core.Models;
using Riok.Mapperly.Abstractions;

namespace API.Mappings;

[Mapper]
public partial class ScheduleMapper :
    IMapper<Module, ModuleResponse, ModuleRequest>,
    IMapper<Lesson, LessonResponse, LessonRequest>
{
    public partial Module Map(ModuleRequest request);

    //[MapProperty([nameof(Attendance.Lesson), nameof(Attendance.Lesson.Date)], nameof(AttendanceResponse.Date))]
    public partial ModuleResponse Map(Module entity);
    public partial IEnumerable<ModuleResponse> Map(IEnumerable<Module> entities);

    public partial Lesson Map(LessonRequest request);
    public partial LessonResponse Map(Lesson entity);
    public partial IEnumerable<LessonResponse> Map(IEnumerable<Lesson> entities);
}