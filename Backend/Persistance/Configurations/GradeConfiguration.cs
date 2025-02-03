using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasKey(t => t.Id);
        // Grade -> Subject (Many to One)
        builder.HasOne(g => g.Subject)
              .WithMany(s => s.Grades);

        builder.HasOne(g => g.Module)
              .WithMany(m => m.Grades);

        // Grade -> Student (Many to One)
        builder.HasOne(g => g.Student)
               .WithMany(s => s.Grades);
    }
}
