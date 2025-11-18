using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.UserProfiles;
public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        // Configure 1:1 relationship with User
        builder.HasOne(n => n.User) // nav
               .WithOne(u => u.Profile) // inverse nav
               .HasForeignKey<UserProfile>(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);           // gets deteled as parent (User) is deleted.

        builder.HasKey(p => p.UserId);

        builder.Property(p => p.Address).IsRequired().HasMaxLength(255);

        builder.Property(p => p.City).IsRequired().HasMaxLength(100);

        builder.Property(p => p.Country).IsRequired().HasMaxLength(100);

        builder.Property(p => p.PostalCode).IsRequired().HasMaxLength(100);

        builder.Property(p => p.ProfileImageUrl).IsRequired().HasMaxLength(255);

        builder.Property(x => x.DateOfBirth).IsRequired().HasColumnType("date"); // date only
    }
}
