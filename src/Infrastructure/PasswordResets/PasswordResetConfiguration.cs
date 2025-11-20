using Domain.PasswordResets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.PasswordResets;

public class PasswordResetConfiguration : IEntityTypeConfiguration<PasswordReset>
{
    public void Configure(EntityTypeBuilder<PasswordReset> builder)
    {
        builder.HasKey(c => c.PrId);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.ExpiresAt).IsRequired();
        builder.Property(c => c.Used);
    }
}
