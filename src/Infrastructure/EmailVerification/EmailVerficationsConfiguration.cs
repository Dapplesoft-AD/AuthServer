using Domain.EmailVerification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EmailVerification;

public class EmailVerificationsConfiguration : IEntityTypeConfiguration<EmailVerifications>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EmailVerifications> builder)
    {
        builder.HasKey(c => c.EvId);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Token).IsRequired().HasMaxLength(255);
        builder.Property(c => c.ExpiresAt).IsRequired();
        builder.Property(c => c.VerifiedAt).IsRequired();
    }
}
