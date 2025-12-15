using Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Projects;
internal sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.ClientId)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(a => a.ClientId)
            .IsUnique();

        builder.Property(a => a.ClientSecret)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(a => a.Domain)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(a => a.RedirectUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(a => a.Status)
            .IsRequired()
            .HasDefaultValue(ProjectStatus.Active)
            .HasConversion<int>(); // Convert enum to int for storage
    }
}
