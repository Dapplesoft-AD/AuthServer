using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Regions;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => r.CountryId);

        builder.Property(r => r.Id).IsRequired();
        builder.Property(r => r.CountryId).IsRequired();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.RegionType).IsRequired().HasMaxLength(100);
        builder.Property(r => r.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("NOW()");
        builder.Property(r => r.UpdatedAt).IsRequired(false);

        builder.HasOne(r => r.Country)
       .WithMany()  
       .HasForeignKey(r => r.CountryId);
    }
}
