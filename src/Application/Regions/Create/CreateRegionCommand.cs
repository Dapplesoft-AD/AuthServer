using System;
using Application.Abstractions.Messaging;

namespace Application.Regions.Create;

public sealed class CreateRegionCommand : ICommand<Guid>
{
    public Guid CountryId { get; set; }
    public string Name { get; set; }
    public string RegionType { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
