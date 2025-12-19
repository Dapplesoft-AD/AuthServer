using Application.Abstractions.Data;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore;
using SharedKernel.Models;

namespace Infrastructure.Database;

public sealed class OpenIddictDbContext : DbContext
{
    public OpenIddictDbContext(
        DbContextOptions<OpenIddictDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseOpenIddict();
        //modelBuilder.HasDefaultSchema("auth");
    }
}
