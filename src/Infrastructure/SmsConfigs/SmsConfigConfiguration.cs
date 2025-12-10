using Domain.SmsConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SmsConfigs;

internal sealed class SmsConfigConfiguration : IEntityTypeConfiguration<SmsConfig>
{
    public void Configure(EntityTypeBuilder<SmsConfig> builder)
    {
        builder.HasKey(e => e.SmsId);
        builder.Property(e => e.SmsToken)
               .IsRequired()
               .HasMaxLength(4);
    }
}
