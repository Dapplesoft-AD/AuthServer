using Domain.AuditLogs;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.AuditLogs;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(a => a.UserId)
            .IsRequired(false);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(a => a.BusinessId)
            .IsRequired(false); 

        builder.Property(a => a.Action)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Description)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(a => a.CreatedAt)
            .IsRequired();

        builder.Property(a => a.UpdatedAt)
            .IsRequired(false);

        // Indexes
        builder.HasIndex(a => a.UserId);
        builder.HasIndex(a => a.BusinessId);
        builder.HasIndex(a => a.CreatedAt);
    }
}
