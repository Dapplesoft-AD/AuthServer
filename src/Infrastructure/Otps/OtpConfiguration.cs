using Domain.Otps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Otps;

internal sealed class OtpConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.ToTable("Otps");

        // Primary Key
        builder.HasKey(t => t.Id);

        // Properties
        builder.Property(t => t.Id)
               .ValueGeneratedNever();

        builder.Property(t => t.OtpToken)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(t => t.Destination)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(t => t.ExpiresAt)
               .IsRequired();

        builder.Property(t => t.Delay)
                .IsRequired()
                .HasColumnType("interval");

        builder.Property(t => t.OtpType)
               .IsRequired()
               .HasConversion<int>();

        builder.Property(t => t.IsExpired)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(t => t.IsUsed)
               .IsRequired()
               .HasDefaultValue(false);

        // Indexes (important for OTP validation performance)
        builder.HasIndex(t => t.Destination);
        builder.HasIndex(t => new 
        {
            t.Destination,
            t.OtpType,
            t.OtpToken,
            t.ExpiresAt
        });
    }
}
