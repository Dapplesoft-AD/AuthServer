using System.Text.Json.Serialization;

namespace Application.Locations.GetAll;

public sealed class GetLocationQueryResponse 
{
    public CountryDto Country { get; set; }
}

public class CountryDto
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }

    [JsonPropertyOrder(2)]
    public string Name { get; set; } = null!;

    [JsonPropertyOrder(3)]
    public List<RegionDto>  Regions{ get; set; }
}

public class RegionDto
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }

    [JsonPropertyOrder(2)]
    public string Name { get; set; } = null!;

    [JsonPropertyOrder(3)]
    public List<DistrictDto> Districts { get; set; }
}

public class DistrictDto
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }

    [JsonPropertyOrder(2)]
    public string Name { get; set; } = null!;

    [JsonPropertyOrder(3)]
    public List<SubDistrictDto> SubDistricts { get; set; }
}

public class SubDistrictDto 
{
    [JsonPropertyOrder(1)]
    public Guid Id { get; set; }

    [JsonPropertyOrder(2)]
    public string Name { get; set; } = null!;
}
