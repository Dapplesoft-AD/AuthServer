using System;
using Application.Abstractions.Messaging;

namespace Application.Districts.Create;

public sealed class CreateDistrictCommand : ICommand<Guid>
{
    public Guid RegionId { get; set; }
    public string Name { get; set; }
}
