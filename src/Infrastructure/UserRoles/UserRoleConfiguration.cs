using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UserRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.UserRoles;
internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId, ur.ApplicationId });

        builder.HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles) // Updated: Add navigation property
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Role)
            .WithMany() // Role can have multiple UserRoles
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ur => ur.Application)
            .WithMany() // Application can have multiple UserRoles
            .HasForeignKey(ur => ur.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ur => ur.AssignedAt)
            .IsRequired();
    }
}
