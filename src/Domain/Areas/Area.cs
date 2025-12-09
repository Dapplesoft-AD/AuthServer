using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Areas;

public sealed class Area
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public Guid DistrictId { get; set; }

    public string Name { get; set; } = string.Empty;

    public AreaType Type { get; set; }

    public bool IsActive { get; set; }

    public enum AreaType
    {
        Upazila = 1,
        City = 2,
        Thana = 3,
        Municipality = 4,
        Township = 5
    }
}
