using SharedKernel;

namespace Domain.Regions;

public class Region : Entity
{
    public Guid Id { get; set; }
    public Guid CountryId { get; set; }
    public string Name { get; set; }
    public bool? IsNew { get; set; }
}
