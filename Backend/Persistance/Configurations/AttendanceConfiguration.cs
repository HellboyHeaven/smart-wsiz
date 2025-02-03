using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;

public class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {

        builder.HasKey(a => a.Id);


        builder.HasOne(a => a.Lesson);

        builder.HasOne(a => a.Student)
               .WithMany(s => s.Attendances);



    }
}