using System;
using Application.Abstractions.Messaging;

namespace Application.Districts.Update;

public sealed record UpdateDistrictCommand(
    Guid DistrictId,
    Guid RegionId,
    Guid CountryId,
    string Name,
    bool IsActive
) : ICommand;
