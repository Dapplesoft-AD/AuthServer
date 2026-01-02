using System;

namespace Application.Districts.Get;

public sealed class DistrictResponse
{
    public Guid Id { get; set; }
    public Guid RegionId { get; set; }
    public string Name { get; set; }
    public bool? IsNew { get; set; }
}
