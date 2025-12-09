namespace Domain.Localities;

public sealed class Locality
{
    public Guid Id { get; set; }

    public Guid CountryId { get; set; }

    public Guid AreaId { get; set; }

    public string Name { get; set; } = string.Empty;

    public LocalityType Type { get; set; }

    public bool IsActive { get; set; }

    public enum LocalityType
    {
        Union = 1,
        Ward = 2,
        Neighborhood = 3,
        Village = 4,
        Town = 5,
        Parish = 6,
        Suburb = 7,
        Hamlet = 8
    }
}
