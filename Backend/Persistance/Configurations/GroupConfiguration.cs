using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(t => t.Id);

        builder.HasMany(g => g.Students)
               .WithOne(s => s.Group);

        builder.HasOne(g => g.Course)
          .WithMany();

        builder.Navigation(p => p.Course)
            .AutoInclude();

    }
}