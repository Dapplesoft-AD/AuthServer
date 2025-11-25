using Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Roles;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.RoleName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description);

        builder.HasIndex(x => x.RoleName)
            .IsUnique();
    }
}
