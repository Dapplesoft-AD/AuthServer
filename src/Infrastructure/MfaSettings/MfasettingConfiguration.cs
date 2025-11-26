using Domain.MfaSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.MfaSettings;

public class MfaSettingConfiguration : IEntityTypeConfiguration<MfaSetting>
{
    public void Configure(EntityTypeBuilder<MfaSetting> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasOne(m => m.User)
               .WithMany()
               .HasForeignKey(m => m.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.UserId);

        builder.Property(m => m.Id).IsRequired();
        builder.Property(m => m.UserId).IsRequired();
        builder.Property(m => m.SecretKey).HasMaxLength(255).IsRequired();
        builder.Property(m => m.BackupCodes).HasColumnType("text").IsRequired(false);
        builder.Property(m => m.Enabled).IsRequired().HasDefaultValue(false);

        builder.Property(m => m.Method)
               .HasConversion<int>()
               .IsRequired();
    }
}
