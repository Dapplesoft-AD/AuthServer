using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Token;

public class TokenConfiguration : IEntityTypeConfiguration<Tokens>
{
    public void Configure(EntityTypeBuilder<Tokens> builder)
    {
        builder.HasKey(c => c.TokenId);
        builder.HasIndex(c => c.User_id);
        builder.Property(c => c.App_id).IsRequired();
        builder.Property(c => c.Access_token).IsRequired().HasColumnType("text");
        builder.Property(c => c.Refresh_token).IsRequired().HasColumnType("text");
        builder.Property(c => c.Issued_at).IsRequired();
    }
}
