using Domain.Countries;
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
        builder.HasIndex(r => r.Name);

        builder.Property(r => r.Id).IsRequired();
        builder.Property(r => r.CountryId).IsRequired();
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);

        builder.HasOne<Country>()
            .WithMany()
            .HasForeignKey(r => r.CountryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
