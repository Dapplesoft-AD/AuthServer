using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.ForgetPasswordReset;

internal sealed class ForgetPasswordResetCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher)
    : ICommandHandler<ForgetPasswordResetCommand, ForgetPasswordResetResponse>
{
    public async Task<Result<ForgetPasswordResetResponse>> Handle(
        ForgetPasswordResetCommand command,
        CancellationToken cancellationToken)
    {
        // Validate passwords match
        if (command.NewPassword != command.ConfirmPassword)
        {
            return Result.Failure<ForgetPasswordResetResponse>(
                Error.Failure("Password.Mismatch", "New password and confirmation do not match"));
        }

        // Get user by email
        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {            
            return new ForgetPasswordResetResponse(
                Success: true,
                Message: "If your email exists in our system, password has been reset successfully."
            );
        }

        string newPasswordHash = passwordHasher.Hash(command.NewPassword);

        user.PasswordHash = newPasswordHash;
        user.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return new ForgetPasswordResetResponse(
            Success: true,
            Message: "Password has been successfully reset. You can now login with your new password."
        );
    }
}
