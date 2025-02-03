using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations;
public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.HasKey(t => t.Id);
        // Module -> Students (One to Many)
        builder.HasMany(g => g.Students)
               .WithMany(s => s.Modules);

        // Module -> Subject (One to One)
        builder.HasOne(g => g.Subject)
               .WithMany(s => s.Modules);

        builder.HasOne(g => g.Teacher)
               .WithMany(t => t.Modules);

        builder.HasMany(m => m.Groups)
            .WithMany();

        builder.HasMany(m => m.Lessons)
            .WithOne(l => l.Module);
    }
}