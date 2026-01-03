using Domain.Roles;
using Domain.Users;
using Domain.UserRoles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed;

internal static class UsersAndRoleSeed
{
    internal static void Apply(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(Roles());
        modelBuilder.Entity<User>().HasData(Users());
        modelBuilder.Entity<UserRole>().HasData(UserRoles());
    }

    private static Role[] Roles() =>
    [
        new Role
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            RoleName = "Administrator",
            RoleCode = RoleCode.Admin,
            Description = "System Administrator with full access"
        },
        new Role
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            RoleName = "Support",
            RoleCode = RoleCode.Support,
            Description = "Support Engineers"
        },
        new Role
        {
            Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            RoleName = "Analytics",
            RoleCode = RoleCode.Analytics,
            Description = "Helps in Analysis"
        },
        new Role
        {
            Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            RoleName = "PaymentAdmin",
            RoleCode = RoleCode.PaymentAdmin,
            Description = "Asses the payments"
        },
        new Role
        {
            Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            RoleName = "Common Usser",
            RoleCode = RoleCode.PublicUser,
            Description = "Common/Normal User"
        }
    ];

    private static User[] Users() =>
    [
        new User
        {
            Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            Email = "admin@auth.dapplesoft.com",
            FullName = "Default Admin",
            PasswordHash = "60358AD3245A0E1D8FC2CA0B0914E45C5F87143DDB2C9E81E09B4E41676F30B8-99D093AF2C44DB8DDCA9FE77BDE4A9F2", // admin12345
            CreatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc),
            UpdatedAt = new DateTime(2025, 12, 16, 0, 0, 0, DateTimeKind.Utc)
        }
    ];

    private static UserRole[] UserRoles() =>
    [
        new UserRole
        {
            Id = Guid.Parse("aaaaaaaa-eeee-ffff-ffff-ffffffffffff"),
            UserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            RoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")
        }
    ];
}
