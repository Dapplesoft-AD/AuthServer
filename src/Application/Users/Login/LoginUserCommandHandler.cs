using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Common;
using Domain.Roles;
using Domain.UserRoles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    //ITokenProvider tokenProvider,
    IUserSession userSession
    ) : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        string? emailLower = Normalizer.EmailAddressLowerCase(command.Email);
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == emailLower, cancellationToken);

        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<LoginUserResponse>("Wrong Credentials");
        }

        UserRole? userRole = await context.UserRoles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.UserId == user.Id, cancellationToken);

        string roleCode = string.Empty;
        if (userRole is not null)
        {
            Role? role = await context.Roles
            .AsNoTracking()
            .SingleOrDefaultAsync(r => r.Id == userRole.RoleId, cancellationToken);
            
            if(role is not null)
            {
                roleCode = role.RoleCode;
            }
        }

        // Sign in the user (cookie session)
        await userSession.SignInAsync(user, cancellationToken);

        var response = new LoginUserResponse(
            Token: "",                // tokenProvider.Create(user),
            RefreshToken: "",
            User: new LogInUserInfo(
                Id: user.Id,
                RoleCode: roleCode
            )
        );
        return response;
    }
}
