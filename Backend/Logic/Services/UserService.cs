using Application.Attributes;
using Core.Interfaces;
using Core.Models.Users;

namespace Application.Services;

[Service]
public class UserService(IRepository<User> userRepository)
{
    public async Task<User?> GetByIdAsync(Guid id) => await userRepository.GetByIdAsync(id);
}
