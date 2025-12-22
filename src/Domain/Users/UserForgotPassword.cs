using SharedKernel;

namespace Domain.Users;

public class UserForgotPassword
{
    public Result ResetUserPassword(User user, string newPasswordHash)
    {
        if (user is null)
        {
            return Result.Failure(
                new Error("User.NotFound", "User not found", ErrorType.NotFound));
        }

        if (user.Status != UserStatus.Active)
        {
            return Result.Failure(
                new Error("User.Inactive", "Cannot reset password for inactive user", ErrorType.Validation));
        }       

        if (string.IsNullOrWhiteSpace(newPasswordHash))
        {
            return Result.Failure(
                new Error("User.InvalidPassword", "Password hash cannot be empty", ErrorType.Validation));
        }

        if (newPasswordHash == user.PasswordHash)
        {
            return Result.Failure(
                new Error("User.SamePassword",
                "New password cannot be the same as current password", ErrorType.Validation));
        }

        user.PasswordHash = newPasswordHash;
        user.UpdatedAt = DateTime.UtcNow;

        return Result.Success();
    }
}
