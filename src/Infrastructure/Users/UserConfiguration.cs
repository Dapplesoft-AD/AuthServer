using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Email).HasMaxLength(150).IsRequired();

        builder.Property(u => u.FullName).HasMaxLength(100).IsRequired();

        builder.Property(u => u.Phone).HasMaxLength(20);

        builder.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();

        builder.Property(u => u.IsEmailVerified).IsRequired();

        builder.Property(u => u.IsMFAEnabled).IsRequired();

        builder.Property(u => u.Status).IsRequired();

        builder.Property(u => u.CreatedAt).IsRequired();

        builder.Property(u => u.UpdatedAt).IsRequired();
    }
}
