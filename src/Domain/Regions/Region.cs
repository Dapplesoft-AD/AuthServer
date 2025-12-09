using System;
using Domain.Countries;
using SharedKernel;

namespace Domain.Regions;

public class Region : Entity
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }

    public string Name { get; set; }
    public string RegionType { get; set; }
    public bool IsActive { get; set; }
    public Country Country { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
