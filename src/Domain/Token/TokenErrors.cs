using SharedKernel;

namespace Domain.Token;

public static class TokenErrors
{
    public static Error AlreadyCompleted(Guid TokenId) => Error.Problem(
        "Token.AlreadyCompleted",
        $"The token with Id = '{TokenId}' is already completed.");

    public static Error NotFound(Guid TokenId) => Error.NotFound(
        "Tokens.NotFound",
        $"The token with the Id = '{TokenId}' was not found");

}
