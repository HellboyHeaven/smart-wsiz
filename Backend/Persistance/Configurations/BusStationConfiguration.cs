using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class BusStationConfiguration : IEntityTypeConfiguration<BusStation>
{
    public void Configure(EntityTypeBuilder<BusStation> builder)
    {
        builder.HasKey(s => s.Id);
    }
}