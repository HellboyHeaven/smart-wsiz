using Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne(r => r.User)
            .WithOne()
            .HasForeignKey<RefreshToken>(r => r.UserId)
            .IsRequired();

    }
}