using Domain.Users;

namespace Application.Users.GetAll;

// this is actually model, response is list of this model
public sealed class GetAllUsersQueryResponse
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
