using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Customers;
using Domain.Supliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Supliers;
public class SuplierConfiguration : IEntityTypeConfiguration<Suplier>
{
    public void Configure(EntityTypeBuilder<Suplier> builder)
    {
        builder.HasIndex(c => c.Email).IsUnique();

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Address).HasMaxLength(200);
    }
}
