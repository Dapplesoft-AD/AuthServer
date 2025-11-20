using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.MfaLogs;

public class MfaLogConfiguration : IEntityTypeConfiguration<MfaLog>
{
    public void Configure(EntityTypeBuilder<MfaLog> builder)
    {
        builder.ToTable("mfa_logs");

        builder.HasKey(m => m.Id);

        // 🔗 Foreign key: user_id → users.id
        builder.Property(m => m.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(m => m.Id)
               .HasColumnName("id")
               .IsRequired();

        builder.Property(m => m.LoginTime)
               .HasColumnName("login_time")
               .IsRequired();

        builder.Property(m => m.IpAddress)
               .HasColumnName("ip_address")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(m => m.Device)
               .HasColumnName("device")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(m => m.Status)
               .HasColumnName("status")
               .HasConversion<string>()      
               .HasMaxLength(20)
               .IsRequired();

    }
}
