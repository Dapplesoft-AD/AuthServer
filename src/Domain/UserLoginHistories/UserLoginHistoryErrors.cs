using SharedKernel;

namespace Domain.UserLoginHistories;

public static class UserloginHistoryErrors
{

    public static Error NotFound(Guid Id) => Error.NotFound(
        "UserloginHistory.NotFound",
        $"The user history with the Id = '{Id}' was not found");
}

