using Core.Models.Users;
using Persistance.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories;

[Repository]
public class UserRepository(UniversityDbContext context) : Repository<User>(context)
{
    public override async Task UpdateAsync(User entity, Expression<Func<User, bool>> filter = null, CancellationToken cancellationToken = default)
    {
       if (string.IsNullOrWhiteSpace(entity.PasswordHash))
       {
            var user = await GetByIdAsync(entity.Id);
            entity.PasswordHash = user?.PasswordHash ?? "";
       }
        await base.UpdateAsync(entity, filter, cancellationToken);
    }
}
