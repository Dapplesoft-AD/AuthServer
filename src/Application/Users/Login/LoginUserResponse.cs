
namespace Application.Users.Login;

public sealed record LoginUserResponse(
    string Token,
    string RefreshToken,
    LogInUserInfo User
);

public sealed record LogInUserInfo(
    Guid Id
);
