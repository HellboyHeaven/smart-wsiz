using API.Contracts.Requests.Curriculum;
using API.Contracts.Responses.Curriculum;
using Core.Interfaces;
using Core.Models;
using Riok.Mapperly.Abstractions;


namespace API.Mappings;


[Mapper]
public partial class CurriculumMapper :
    IMapper<Course, CourseResponse, CourseRequest>,
    IMapper<Group, GroupResponse, GroupRequest>,
    IMapper<Subject, SubjectResponse, SubjectRequest>
{
    #region Course
    public partial Course Map(CourseRequest request);
    public partial CourseResponse Map(Course entity);
    public partial IEnumerable<CourseResponse> Map(IEnumerable<Course> response);
    #endregion

    #region Group
    public partial Group Map(GroupRequest request);

    [MapProperty(nameof(Group), nameof(GroupResponse.Code))]
    public partial GroupResponse Map(Group entity);
    public partial IEnumerable<GroupResponse> Map(IEnumerable<Group> response);
    #endregion

    #region Subject
    public partial Subject Map(SubjectRequest request);
    public partial SubjectResponse Map(Subject entity);
    public partial IEnumerable<SubjectResponse> Map(IEnumerable<Subject> entities);

    #endregion


}
