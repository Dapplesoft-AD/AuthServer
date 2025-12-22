using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Users.ForgotPasswordReset;

internal sealed class ForgotPasswordResetCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher)
    : ICommandHandler<ForgotPasswordResetCommand, ForgotPasswordResetResponse>
{
    public async Task<Result<ForgotPasswordResetResponse>> Handle(
        ForgotPasswordResetCommand command,
        CancellationToken cancellationToken)
    {
        if (command.NewPassword != command.ConfirmPassword)
        {
            return Result.Failure<ForgotPasswordResetResponse>(
                new Error("Password.Mismatch",
                "New password and confirmation do not match",
                ErrorType.Validation));
        }

        User? user = await context.Users
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return new ForgotPasswordResetResponse(
                Success: true,
                Message: "If your email exists in our system, password has been reset successfully."
            );
        }

        string newPasswordHash = passwordHasher.Hash(command.NewPassword);

        var passwordService = new UserForgotPassword();
        Result resetResult = passwordService.ResetUserPassword(user, newPasswordHash);

        if (resetResult.IsFailure)
        {
            return Result.Failure<ForgotPasswordResetResponse>(resetResult.Error);
        }

        await context.SaveChangesAsync(cancellationToken);

        return new ForgotPasswordResetResponse(
            Success: true,
            Message: "If your email exists in our system, password has been reset successfully."
        );
    }
}
