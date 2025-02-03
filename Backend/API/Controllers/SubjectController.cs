using API.Contracts.Requests.Curriculum;
using API.Contracts.Responses.Curriculum;
using API.Mappings;
using Core.Models;

namespace API.Controllers;


public class SubjectController() : ApiControllerBase<Subject, SubjectResponse, SubjectRequest, CurriculumMapper>
{

}
