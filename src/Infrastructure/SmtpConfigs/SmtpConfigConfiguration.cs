using Domain.SmtpConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SmtpConfigs;

internal sealed class SmtpConfigConfiguration : IEntityTypeConfiguration<SmtpConfig>
{
    public void Configure(EntityTypeBuilder<SmtpConfig> builder)
    {
        builder.HasKey(t => t.SmtpId);
        builder.Property(t => t.Username).IsRequired().HasMaxLength(256);
        builder.Property(t => t.SenderEmail).IsRequired().HasMaxLength(256);
        builder.Property(t => t.Password).IsRequired().HasMaxLength(16);
    }
}
