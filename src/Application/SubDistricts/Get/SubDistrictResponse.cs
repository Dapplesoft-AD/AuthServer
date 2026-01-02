namespace Application.SubDistricts.Get;

public sealed record SubDistrictResponse(Guid Id, Guid DistrictId, string Name, bool IsNew);
