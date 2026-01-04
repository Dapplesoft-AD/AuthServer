using Domain.Countries;
using Domain.Districts;
using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Seed.AddressSeed;

internal class Configurations
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Name);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        }
    }

    public class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasIndex(r => r.Name);

            builder.HasIndex(r => r.CountryId);

            builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        }
    }

    public class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasIndex(d => d.Name);

            builder.HasIndex(d => d.RegionId);

            builder.Property(d => d.Name).IsRequired().HasMaxLength(50);
        }
    }
}
