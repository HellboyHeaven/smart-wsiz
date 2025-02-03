using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id)
        .ValueGeneratedOnAdd();

        builder.HasIndex(u => u.Login).IsUnique();

        builder.HasOne(u => u.RefreshToken).
            WithOne(r => r.User);


    }
}