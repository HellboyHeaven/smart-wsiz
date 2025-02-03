using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(t => t.Id);
        // Lesson -> Module (Many to One)
        builder.HasOne(l => l.Module)
               .WithMany(g => g.Lessons);

    }
}

