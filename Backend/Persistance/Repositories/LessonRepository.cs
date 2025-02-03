using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories;

[Repository]
public class LessonRepository(UniversityDbContext context) : Repository<Lesson>(context)
{
    protected override Func<IQueryable<Lesson>, IQueryable<Lesson>> Included
        => q => q
        .Include(l => l.Module).ThenInclude(m => m.Teacher)
        .Include(l => l.Module.Subject)
        .Include(l => l.Module.Groups);
}
