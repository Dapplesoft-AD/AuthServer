using System;
using Domain.Countries;
using Domain.Regions;
using SharedKernel;

namespace Domain.Districts;

public class District : Entity
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public Guid RegionId { get; set; }

    public string Name { get; set; }
    public bool IsActive { get; set; }
    public Country Country { get; set; }
    public Region Region { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
