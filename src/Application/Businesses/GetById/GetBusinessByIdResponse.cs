using Domain.Businesses;

namespace Application.Businesses.GetById;

public sealed class GetBusinessByIdResponse
{
    public Guid Id { get; set; }
    public Guid OwnerUserId { get; set; }
    public string BusinessName { get; set; }
    public string IndustryType { get; set; }
    public string LogoUrl { get; set; }
    public BusinessStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
