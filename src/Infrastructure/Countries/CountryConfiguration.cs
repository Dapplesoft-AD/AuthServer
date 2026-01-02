using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Countries;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("countries");
        
        builder.HasKey(x => x.Id);

        builder.HasIndex(r => r.Name);

        builder.Property(x => x.Id)
               .IsRequired();

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.Capital)
               .HasMaxLength(150);

        builder.Property(x => x.PhoneCode)
               .HasMaxLength(20);

        builder.Property(x => x.IsActive)
               .IsRequired();
    }
}
