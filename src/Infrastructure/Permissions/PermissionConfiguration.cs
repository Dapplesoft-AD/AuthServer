using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Permissions;
internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
            .HasMaxLength(100) // varchar(100)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnType("text") // text type for description
            .IsRequired();

        builder.HasIndex(p => p.Code)
            .IsUnique();
    }
}
