using SharedKernel;

namespace Domain.PasswordResets;

public static class PasswordResetsErrors
{
    public static Error AlreadyCompleted(Guid PrId) => Error.Problem(
        "PasswordReset.AlreadyCompleted",
        $"The Password reset with Id = '{PrId}' is already completed.");

    public static Error NotFound(Guid PrId) => Error.NotFound(
        "PasswordReset.NotFound",
        $"The password reset with the Id = '{PrId}' was not found");
}
