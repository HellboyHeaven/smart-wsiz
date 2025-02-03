using Application.Attributes;
using Core.Interfaces;
using Core.Models;
using Core.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.Auth;
public abstract record UpdateUserCommand(User User) : ICommand;
public record UpdateStudentCommand(Student Student, Guid CourseId) : UpdateUserCommand(Student);
public record UpdateTeacherCommand(Teacher Teacher) : UpdateUserCommand(Teacher);
public record UpdateAdminCommand(Admin Admin) : UpdateUserCommand(Admin);

[Service]
public class UpdateUser(IRepository<User> userRepository, IRepository<Course> courseRepository) : ICommandHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = command.User;
        if (user is Student student && command is RegisterStudentCommand studentCommand)
        {
            student.Course = await courseRepository.GetByIdAsync(studentCommand.CourseId, cancellationToken: cancellationToken);
        }
    


        await userRepository.AddAsync(command.User);
    }
}
