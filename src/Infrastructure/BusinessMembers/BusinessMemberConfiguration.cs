using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.BusinessMembers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BusinessMembers;
public class BusinessMemberConfiguration : IEntityTypeConfiguration<BusinessMember>
{
    public void Configure(EntityTypeBuilder<BusinessMember> builder)
    {
        builder.ToTable("business_members");

        builder.HasKey(bm => bm.Id);

        builder.Property(bm => bm.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(bm => bm.BusinessId)
            .HasColumnName("business_id")
            .IsRequired();

        builder.Property(bm => bm.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(bm => bm.RoleId)
            .HasColumnName("role_id")
            .IsRequired();

        builder.Property(bm => bm.JoinedAt)
            .HasColumnName("joined_at")
            .IsRequired();
    }
}
