using Domain.SubDistricts;
using Domain.Localities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Localities;

internal sealed class LocalityConfiguration : IEntityTypeConfiguration<Locality>
{
    public void Configure(EntityTypeBuilder<Locality> builder)
    {
        // Primary Key
        builder.HasKey(l => l.Id);

        // Properties configuration
        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(l => l.CountryId)
            .IsRequired();

        builder.Property(l => l.AreaId)
            .IsRequired();

        builder.Property(l => l.Type)
            .IsRequired()
            .HasConversion<int>(); 

        builder.Property(l => l.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // Indexes for performance
        builder.HasIndex(l => l.Name);
        builder.HasIndex(l => l.CountryId);
        builder.HasIndex(l => l.AreaId);
        builder.HasIndex(l => l.Type);
        builder.HasIndex(l => l.IsActive);

        // Composite indexes for common query patterns
        builder.HasIndex(l => new { l.AreaId, l.IsActive });
        builder.HasIndex(l => new { l.CountryId, l.AreaId, l.IsActive });
        builder.HasIndex(l => new { l.AreaId, l.Type, l.IsActive });

        builder.HasIndex(l => new { l.AreaId, l.Name })
            .IsUnique();

        builder.HasOne<Domain.SubDistricts.SubDistrict>()
            .WithMany()
            .HasForeignKey(l => l.AreaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
