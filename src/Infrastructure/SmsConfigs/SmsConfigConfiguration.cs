using Domain.SmsConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SmsConfigs;

internal sealed class SmsConfigConfiguration : IEntityTypeConfiguration<SmsConfig>
{
    public void Configure(EntityTypeBuilder<SmsConfig> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.ProviderName)
            .HasMaxLength(50);

        builder.Property(e => e.ProviderUrl);

        builder.Property(e => e.Token)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(e => e.ApiUrl)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(e => e.IsActive)
            .IsRequired();
    }
}
