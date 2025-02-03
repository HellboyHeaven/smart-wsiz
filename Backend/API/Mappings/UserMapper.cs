using API.Contracts.Requests.Auth;
using API.Contracts.Responses.Auth;
using Application.Services;
using Core.Interfaces;
using Core.Models.Users;
using Riok.Mapperly.Abstractions;

namespace API.Mappings;


[Mapper]
public partial class UserMapper : IMapper<User, UserResponse, UserRequest>
{
    #region User
    [MapDerivedType<StudentRequest, Student>]
    [MapDerivedType<TeacherRequest, Teacher>]
    [MapDerivedType<AdminRequest, Admin>]
    [MapProperty(nameof(UserRequest.Password), nameof(User.PasswordHash), Use = nameof(MapPassword))]
    [MapperIgnoreTarget(nameof(StudentRequest.Course))]
    public partial User Map(UserRequest request);



    [MapDerivedType<Student, StudentResponse>]
    [MapDerivedType<Teacher, TeacherResponse>]
    //[MapDerivedType<Admin, AdminResponse>]
    //[MapProperty([nameof(Student.Course), nameof(Student.Course.Name)], nameof(StudentResponse.Course))]
    public partial UserResponse Map(User user);

    [MapDerivedType<IEnumerable<Student>, IEnumerable<StudentResponse>>]
    [MapDerivedType<IEnumerable<Teacher>, IEnumerable<TeacherResponse>>]
    public partial IEnumerable<UserResponse> Map(IEnumerable<User> entities);

    [UserMapping(Default = false)]
    private string MapPassword(string? password)
     => password == null ? "" : PasswordHasher.HashPassword(password) ;
    #endregion




}