
namespace Application.UserProfiles.Get;

public sealed class UserProfileResponse
{
    public Guid UserId { get; set; } // Fk of User.Id
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string ProfileImageUrl { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
