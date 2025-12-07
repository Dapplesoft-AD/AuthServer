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

        // Configure ID (let DB generate it)
        builder.Property(a => a.Id)
            .HasDefaultValueSql("gen_random_uuid()");
        // OR if you want to generate in code:
        // .ValueGeneratedNever(); // Then generate Guid.NewGuid() in code

        // ✅ UserId is nullable for unauthenticated users
        builder.Property(a => a.UserId)
            .IsRequired(false);

        // ✅ Configure FK to Users (optional relationship)
        // If you have navigation property, use: builder.HasOne(a => a.User)
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired(false) // This is important!
            .OnDelete(DeleteBehavior.SetNull);

        // ✅ BusinessId should also be nullable
        builder.Property(a => a.BusinessId)
            .IsRequired(false); // Change from IsRequired() to IsRequired(false)

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
