using Domain.Businesses;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Businesses;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BusinessName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IndustryType)
            .HasMaxLength(100);

        builder.Property(x => x.LogoUrl)
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.OwnerUserId)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
