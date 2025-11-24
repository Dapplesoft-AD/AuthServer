using Domain.Businesses;
using Domain.BusinessMembers;
using Domain.Roles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.BusinessMembers;

public class BusinessMemberConfiguration : IEntityTypeConfiguration<BusinessMember>
{
    public void Configure(EntityTypeBuilder<BusinessMember> builder)
    {
        builder.HasKey(bm => bm.Id);

        builder.HasOne<Business>()
            .WithMany()
            .HasForeignKey(bm => bm.BusinessId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(bm => bm.UserId);

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(bm => bm.RoleId);

        builder.Property(bm => bm.JoinedAt)
            .IsRequired();
    }
}