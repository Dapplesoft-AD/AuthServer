using Domain.UserLoginHistories;
using Domain.UserProfiles;
using SharedKernel;

namespace Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public string PasswordHash { get; set; }

    public string? Phone { get; set; }

    public bool IsEmailVarified { get; set; }

    public bool IsMFAEnabled { get; set; }

    public Status Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public UserProfile? Profile { get; set; }       // Navigation property to UserProfile 1:1
    public ICollection<UserLoginHistory> LoginHistories { get; set; } = [];    // Navigation collection property to LoginHistory 1:N
}
