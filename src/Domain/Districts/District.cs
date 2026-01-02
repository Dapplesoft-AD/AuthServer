using SharedKernel;

namespace Domain.Districts;

public class District : Entity
{
    public Guid Id { get; set; }
    public Guid RegionId { get; set; }
    public string Name { get; set; }
    public bool? IsNew { get; set; }
}
