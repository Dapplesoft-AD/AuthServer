using Application.Abstractions.Messaging;
using Domain.Areas;

namespace Application.Areas.Update;

public sealed record UpdateAreaCommand(
    Guid Id,
    Guid CountryId,
    Guid DistrictId,
    string Name,
    Area.AreaType Type,
    bool IsActive
) : ICommand<Guid>;
