using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.MfaSettings;

public class MfaSettingConfiguration : IEntityTypeConfiguration<MfaSetting>
{
    public void Configure(EntityTypeBuilder<MfaSetting> builder)
    {
        builder.ToTable("mfa_settings");

        builder.HasKey(m => m.Id);

        // 🔗 Foreign key: user_id → users.id
        builder.HasOne(m => m.User)
               .WithMany() 
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.SecretKey)
               .HasColumnName("secret_key")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(m => m.BackupCodes)
               .HasColumnName("backup_codes")
               .HasColumnType("text")
               .IsRequired(false);

        builder.Property(m => m.Method)
               .HasColumnName("method")
               .HasConversion<string>() // ✅ Enum → string
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(m => m.Enabled)
               .HasColumnName("enabled")
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(m => m.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(m => m.Id)
               .HasColumnName("id")
               .IsRequired();
    }
}
