using System;

namespace Application.Regions.Get;

public sealed class RegionResponse
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public string Name { get; set; }
    public bool? IsNew { get; set; }
}
