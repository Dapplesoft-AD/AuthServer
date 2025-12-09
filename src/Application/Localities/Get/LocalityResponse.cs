using System;

namespace Application.Localities.Get;

public sealed record LocalityResponse(
    Guid Id,
    Guid CountryId,
    Guid AreaId,
    string Name,
    int Type,
    string TypeName,
    bool IsActive
);
