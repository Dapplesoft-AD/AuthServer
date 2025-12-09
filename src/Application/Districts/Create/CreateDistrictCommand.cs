using System;
using Application.Abstractions.Messaging;

namespace Application.Districts.Create;

public sealed class CreateDistrictCommand : ICommand<Guid>
{
    public Guid CountryId { get; set; }
    public Guid RegionId { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
