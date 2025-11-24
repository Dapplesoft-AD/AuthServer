using Domain.Users;

namespace Application.Users.GetById;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string Email { get; init; }

    public string FullName { get; init; }

    public string? Phone { get; init; }

    public bool IsEmailVerified { get; init; }

    public bool IsMFAEnabled { get; init; }

    public UserStatus Status { get; init; }

    public DateTime? CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}
