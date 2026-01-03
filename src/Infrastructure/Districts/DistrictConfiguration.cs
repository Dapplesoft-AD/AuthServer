using Domain.Districts;
using Domain.Regions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Districts;

public class DistrictConfiguration : IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.HasKey(d => d.Id);
        builder.HasIndex(d => d.RegionId);
        builder.HasIndex(r => r.Name);

        builder.Property(d => d.Id).IsRequired();
        builder.Property(d => d.RegionId).IsRequired();
        builder.Property(d => d.Name).IsRequired().HasMaxLength(200);

        builder.HasOne<Region>()
            .WithMany()       
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
