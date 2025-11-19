using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.AuditLogs;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AuditLogs;
public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);

        // Foreign Keys
        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.BusinessId).IsRequired();

        // Audit properties
        builder.Property(a => a.Action)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(a => a.Description)
               .HasColumnType("text");

        builder.Property(a => a.CreatedAt)
               .IsRequired();

        // Optional: index for faster querying
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.BusinessId);
    }
}
