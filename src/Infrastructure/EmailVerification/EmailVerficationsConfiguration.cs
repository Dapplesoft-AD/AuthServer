using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.EmailVerification;

namespace Infrastructure.EmailVerification;

public class EmailVerificationsConfiguration : IEntityTypeConfiguration<EmailVerifications>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EmailVerifications> builder)
    {
        builder.HasKey(c => c.EvId);
        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Token).IsRequired().HasMaxLength(255);
        builder.Property(c => c.ExpiresAt).IsRequired();
        builder.Property(c => c.VerifiedAt).IsRequired();
    }
}
