using Domain.MfaLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.MfaLogs;

public class MfaLogConfiguration : IEntityTypeConfiguration<MfaLog>
{
    public void Configure(EntityTypeBuilder<MfaLog> builder)
    {
        builder.HasKey(m => m.Id);

        builder.HasIndex(m => m.UserId); // Let EF Core generate the index name dynamically

        builder.Property(m => m.Id).IsRequired();
        builder.Property(m => m.UserId).IsRequired();
        builder.Property(m => m.LoginTime).IsRequired();
        builder.Property(m => m.CreatedAt).IsRequired().HasDefaultValueSql("NOW()");
        builder.Property(m => m.UpdatedAt).IsRequired(false);
        builder.Property(m => m.IpAddress).HasMaxLength(50).IsRequired();
        builder.Property(m => m.Device).HasMaxLength(100).IsRequired();

        builder.Property(m => m.Status)
               .HasConversion<int>()
               .IsRequired();
    }
}
