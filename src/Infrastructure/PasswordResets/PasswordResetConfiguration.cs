using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Token;
using Domain.PasswordResets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.PasswordResets;

public class PasswordResetConfiguration : IentityTypeConfiguration<PasswordReset>
{
    public void Configure(EntityTypeBuilder<PasswordReset> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.User_Id).IsRequired();
        builder.Property(c => c.Expires_at).IsRequired();
        builder.Property(c => c.Used);
    }
}
