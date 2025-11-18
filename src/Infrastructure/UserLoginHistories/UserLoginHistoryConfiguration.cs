using Domain.UserLoginHistories;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Status = Domain.UserLoginHistories.Status;

namespace Infrastructure.UserLoginHistories;

public sealed class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(p => p.UserId);

        // Configure 1:N relationship with User
        builder.HasOne(h => h.User)   // ref nav
               .WithMany(u => u.LoginHistories) // collection nav
               .HasForeignKey(h => h.UserId)
               .OnDelete(DeleteBehavior.Cascade);           // gets deteled as primary is deleted.

        // alternatively
        builder.HasOne<User>().WithMany().HasForeignKey(h => h.UserId).OnDelete(DeleteBehavior.Cascade);   // gets deteled as primary is deleted.

        builder.Property(u => u.UserId).IsRequired();

        builder.Property(u => u.IpAddress).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Country).HasMaxLength(100);

        builder.Property(u => u.City).HasMaxLength(100);

        builder.Property(u => u.Browser).HasMaxLength(100);

        builder.Property(u => u.OS).HasMaxLength(100);

        builder.Property(u => u.Device).HasMaxLength(50);

        builder.Property(u => u.LogInTime).IsRequired();

        builder.Property(u => u.LogoutTime);

        builder.Property(u => u.Status).IsRequired().HasDefaultValue(Status.Succeed); // login successful by default
    }
}
