using Domain;
using Domain.Areas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Areas;

internal sealed class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(a => a.CountryId)
            .IsRequired();

        builder.Property(a => a.DistrictId)
            .IsRequired();

        builder.Property(a => a.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Add indexes
        builder.HasIndex(a => a.DistrictId);
        builder.HasIndex(a => a.CountryId);
        builder.HasIndex(a => a.Name);
        builder.HasIndex(a => a.Type);

        // Foreign key constraint (assuming districts table exists)
        builder.HasOne<Domain.Districts.District>()
            .WithMany()
            .HasForeignKey(a => a.DistrictId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
