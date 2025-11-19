using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.EmailVerification;

namespace Infrastructure.EmailVerfication;

public class EmailVerficationsConfiguration : IEntityTypeConfiguration<EmailVerifications>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<EmailVerifications> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.User_Id).IsRequired();
        builder.Property(c => c.Token).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Expires_at).IsRequired();
        builder.Property(c => c.Verified_at).IsRequired();
    }
}
