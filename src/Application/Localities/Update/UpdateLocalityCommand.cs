using Application.Abstractions.Messaging;
using Domain.Localities;

namespace Application.Localities.Update;

public sealed record UpdateLocalityCommand(
    Guid Id,
    Guid CountryId,
    Guid AreaId,
    string Name,
    Locality.LocalityType Type,
    bool IsActive
) : ICommand<Guid>;
