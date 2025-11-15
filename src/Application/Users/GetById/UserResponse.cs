using Domain.Users;

namespace Application.Users.GetById;

public sealed record UserResponse
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public string? Phone { get; set; }

    public bool IsEmailVarified { get; set; }

    public bool IsMFAEnabled { get; set; }

    public Status Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
