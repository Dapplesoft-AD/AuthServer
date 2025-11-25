using SharedKernel;

namespace Domain.Businesses;

public class Business : Entity
{
    public Guid Id { get; set; }
    public Guid OwnerUserId { get; set; }
    public string BusinessName { get; set; }
    public string IndustryType { get; set; }
    public string? LogoUrl { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
