using System;
using Application.Abstractions.Messaging;
using Domain.Localities;

namespace Application.Localities.Create;

public sealed record CreateLocalityCommand(
    Guid CountryId,
    Guid AreaId,
    string Name,
    Locality.LocalityType Type,
    bool IsActive
) : ICommand<Guid>;
