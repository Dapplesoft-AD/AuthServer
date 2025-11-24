using Domain.Users;
using SharedKernel;

namespace Domain.UserLoginHistories;

public sealed class UserLoginHistory : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string IpAddress { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Browser { get; set; }
    public string OS { get; set; }
    public string Device { get; set; }
    public DateTime LogInTime { get; set; }
    public DateTime? LogoutTime { get; set; }
    public LoginStatus Status { get; set; }   // Default value = login succeed
    public User? User { get; set; }      // Navigation property to User (only used for configuring ER)
}
