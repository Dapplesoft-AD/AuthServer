using System;

namespace Application.Areas.Get;

public sealed record AreaResponse(
    Guid Id,
    Guid CountryId,
    Guid DistrictId,
    string Name,
    int Type,
    string TypeName,
    bool IsActive
);
