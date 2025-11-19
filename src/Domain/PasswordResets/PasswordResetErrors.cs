using SharedKernel;

namespace Domain.PasswordResets;

public static class PasswordResetsErrors
{
    public static Error AlreadyCompleted(Guid Id) => Error.Problem(
        "TodoItems.AlreadyCompleted",
        $"The todo item with Id = '{Id}' is already completed.");

    public static Error NotFound(Guid Id) => Error.NotFound(
        "TodoItems.NotFound",
        $"The to-do item with the Id = '{Id}' was not found");
}
