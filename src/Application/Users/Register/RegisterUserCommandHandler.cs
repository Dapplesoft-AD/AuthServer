using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Roles;
using Domain.UserRoles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Globalization;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider,
    IPasswordHasher passwordHasher)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        string? emailLower = command.Email.ToLower(CultureInfo.CurrentCulture);
        if (await context.Users.AnyAsync(u => u.Email == emailLower, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = emailLower,
            FullName = command.FullName,
            PasswordHash = passwordHasher.Hash(command.Password),
            CreatedAt = dateTimeProvider.UtcNow,
            UpdatedAt = dateTimeProvider.UtcNow,
            Phone = command.Phone,
            CountryId = command.CountryId,
            RegionId = command.RegionId,
            DistrictId = command.DistrictId,
            SubDistrictId = command.SubDistrictId,
            Status = UserStatus.Active,
            IsEmailVerified = false,
            IsMFAEnabled = false
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        context.Users.Add(user);

        Role? publicUser = await context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.RoleCode == RoleCode.PublicUser, cancellationToken);

        if (publicUser is not null)
        {
            var userRole = new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = publicUser.Id
            };
            context.UserRoles.Add(userRole);
        }

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
