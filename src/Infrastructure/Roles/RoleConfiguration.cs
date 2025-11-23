using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Roles;
internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.RoleName)
            .HasMaxLength(100) 
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnType("text") 
            .IsRequired();

        builder.HasIndex(r => r.RoleName)
            .IsUnique();
    }
}
