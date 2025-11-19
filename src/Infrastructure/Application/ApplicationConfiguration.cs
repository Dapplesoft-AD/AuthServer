using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Application;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Application;
public class ApplicationConfiguration : IEntityTypeConfiguration<Applications>
{
    public void Configure(EntityTypeBuilder<Applications> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Client_id).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Client_secret).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Client_secret).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Api_base_url).IsRequired().HasMaxLength(255);
    }
}
