using Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Applications;
internal sealed class ApplicationConfiguration : IEntityTypeConfiguration<Applicationapply>
{
    public void Configure(EntityTypeBuilder<Applicationapply> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.ClientId)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.ClientSecret)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.RedirectUri)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.ApiBaseUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<int>(); // Convert enum to int for storage

       

        // Optional: Add index for ClientId if it should be unique
        builder.HasIndex(a => a.ClientId)
            .IsUnique();
    }
}
