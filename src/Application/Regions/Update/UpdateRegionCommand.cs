using System;
using Application.Abstractions.Messaging;

namespace Application.Regions.Update;

public sealed record UpdateRegionCommand(
    Guid RegionId,
    Guid CountryId,
    string Name,
    string RegionType,
    bool IsActive
) : ICommand;
