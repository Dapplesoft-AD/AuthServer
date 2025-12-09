using Domain.Otps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Otps;

internal sealed class OtpConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.HasKey(t=>t.OtpId);
        builder.Property(t=>t.OtpToken).IsRequired().HasMaxLength(16);
        builder.Property(t=>t.Email).IsRequired().HasMaxLength(256);
    }
}
