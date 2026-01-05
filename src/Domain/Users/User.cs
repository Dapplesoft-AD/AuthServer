using SharedKernel;

namespace Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public string PasswordHash { get; set; }

    public string? Phone { get; set; }

    public bool IsEmailVerified { get; set; }

    public bool IsMFAEnabled { get; set; }

    public Guid? CountryId { get; set; }

    public Guid? RegionId { get; set; }
    
    public Guid? DistrictId { get; set; }
    
    public Guid? SubDistrictId { get; set; }
    
    public string? Address { get; set; }

    public UserStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
