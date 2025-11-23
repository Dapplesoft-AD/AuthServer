using Domain.UserLoginHistories;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoginStatus = Domain.UserLoginHistories.LoginStatus;

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
                .OnDelete(DeleteBehavior.Cascade);           // gets deleted as primary is deleted.

        builder.Property(u => u.UserId).IsRequired();

        builder.Property(u => u.IpAddress).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Country).IsRequired().HasMaxLength(100);

        builder.Property(u => u.City).IsRequired().HasMaxLength(100);

        builder.Property(u => u.Browser).IsRequired().HasMaxLength(100);

        builder.Property(u => u.OS).IsRequired().HasMaxLength(100);

        builder.Property(u => u.Device).IsRequired().HasMaxLength(50);

        builder.Property(u => u.LogInTime).IsRequired();

        builder.Property(u => u.LogoutTime);

        builder.Property(u => u.Status).IsRequired().HasDefaultValue(LoginStatus.Succeed); // login successful by default
    }
}
