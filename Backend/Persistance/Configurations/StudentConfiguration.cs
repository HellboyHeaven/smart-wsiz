using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {

        builder.HasIndex(s => s.StudentId).IsUnique();
        // Student -> Modules (Many to Many)
        builder.HasMany(s => s.Modules)
               .WithMany(m => m.Students);


        builder.ToTable("Students");

    }
}
