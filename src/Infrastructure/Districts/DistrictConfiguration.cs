using Domain.Districts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Districts;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.CountryId);
        builder.HasIndex(d => d.RegionId);

        builder.Property(d => d.Id).IsRequired();
        builder.Property(d => d.CountryId).IsRequired();
        builder.Property(d => d.RegionId).IsRequired();
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
        builder.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(d => d.CreatedAt).IsRequired().HasDefaultValueSql("NOW()");
        builder.Property(d => d.UpdatedAt).IsRequired(false);

        builder.HasOne(d => d.Country)
            .WithMany()       
            .HasForeignKey(d => d.CountryId);

        builder.HasOne(d => d.Region)
            .WithMany()       
            .HasForeignKey(d => d.RegionId);
    }
}
