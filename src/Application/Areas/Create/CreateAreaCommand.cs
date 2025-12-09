using Application.Abstractions.Messaging;
using Domain;
using Domain.Areas;

namespace Application.Areas.Create;

public sealed record CreateAreaCommand(
    Guid CountryId,
    Guid DistrictId,
    string Name,
    Area.AreaType Type,
    bool IsActive
) : ICommand<Guid>;
