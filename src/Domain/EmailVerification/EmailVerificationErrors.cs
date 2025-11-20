using SharedKernel;

namespace Domain.EmailVerification;

public static class EmailVerificationErrors
{
    public static Error AlreadyCompleted(Guid EV_Id) => Error.Problem(
        "Email verification.AlreadyCompleted",
        $"The Email Verification with Id = '{EV_Id}' is already completed.");

    public static Error NotFound(Guid EV_Id) => Error.NotFound(
        "Email verification",
        $"The Email Verification with the Id = '{EV_Id}' was not found");
}
