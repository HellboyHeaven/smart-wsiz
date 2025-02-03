using Application.Attributes;
using Core.Interfaces;
using Core.Models;
using Core.Models.Users;

namespace Application.Handlers.Auth;

public abstract record RegisterUserCommand(User User) : ICommand;
public record RegisterStudentCommand(Student Student, Guid CourseId) : UpdateUserCommand(Student);
public record RegisterTeacherCommand(Teacher Teacher) : UpdateUserCommand(Teacher);
public record RegisterAdminCommand(Admin Admin) : UpdateUserCommand(Admin);

[Service]
public class Register(IRepository<User> userRepository, IRepository<Course> courseRepository) : ICommandHandler<UpdateUserCommand>
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

