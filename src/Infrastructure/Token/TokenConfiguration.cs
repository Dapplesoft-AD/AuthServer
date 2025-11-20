using Domain.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Token;

public class TokenConfiguration : IEntityTypeConfiguration<Tokens>
{
    public void Configure(EntityTypeBuilder<Tokens> builder)
    {
        builder.HasKey(c => c.TokenId);
        builder.HasIndex(c => c.UserId);
        builder.Property(c => c.AppId).IsRequired();
        builder.Property(c => c.Accesstoken).IsRequired().HasColumnType("text");
        builder.Property(c => c.Refreshtoken).IsRequired().HasColumnType("text");
        builder.Property(c => c.IssuedAt).IsRequired();
    }
}
