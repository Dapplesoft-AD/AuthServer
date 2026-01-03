using Domain.Districts;
using Domain.SubDistricts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SubDistricts;

internal sealed class SubDistrictConfiguration : IEntityTypeConfiguration<SubDistrict>
{
    public void Configure(EntityTypeBuilder<SubDistrict> builder)
    {

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.DistrictId)
            .IsRequired();

        builder.Property(a => a.IsNew)
            .IsRequired()
            .HasDefaultValue(true);

        // Add indexes
        builder.HasIndex(a => a.DistrictId);
        builder.HasIndex(a => a.Name);

        // Foreign key constraint (assuming districts table exists)
        builder.HasOne<District>()
            .WithMany()
            .HasForeignKey(a => a.DistrictId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
