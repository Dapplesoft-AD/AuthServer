using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.AuditLogs;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).IsRequired();
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.BusinessId).IsRequired();
        builder.Property(a => a.Action).HasMaxLength(255).IsRequired();
        builder.Property(a => a.Description).HasColumnType("text");
        builder.Property(a => a.CreatedAt).IsRequired();

        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.BusinessId);
    }
}
