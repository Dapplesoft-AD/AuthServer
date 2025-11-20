using SharedKernel;

namespace Domain.PasswordResets;

public static class PasswordResetsErrors
{
    public static Error AlreadyCompleted(Guid PR_Id) => Error.Problem(
        "PasswordReset.AlreadyCompleted",
        $"The Password reset with Id = '{PR_Id}' is already completed.");

    public static Error NotFound(Guid PR_Id) => Error.NotFound(
        "PasswordReset.NotFound",
        $"The password reset with the Id = '{PR_Id}' was not found");
}
