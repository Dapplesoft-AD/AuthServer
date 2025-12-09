using System;

namespace Application.Districts.Get;

public sealed class DistrictResponse
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Guid RegionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
