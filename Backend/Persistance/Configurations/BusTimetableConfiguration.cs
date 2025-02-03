using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;

public class BusTimetableConfiguration : IEntityTypeConfiguration<BusTimetable>
{
    public void Configure(EntityTypeBuilder<BusTimetable> builder)
    {
        builder.HasKey(b => b.Id);

        builder.HasOne(b => b.Station)
            .WithMany(s => s.Timetables);
    }
}