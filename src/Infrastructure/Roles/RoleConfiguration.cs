using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            .HasMaxLength(100) // varchar(100)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasColumnType("text") // text type for description
            .IsRequired();

        builder.HasIndex(r => r.RoleName)
            .IsUnique();
    }
}
