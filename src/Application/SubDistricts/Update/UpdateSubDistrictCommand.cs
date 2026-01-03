using Application.Abstractions.Messaging;

namespace Application.SubDistricts.Update;

public sealed record UpdateSubDistrictCommand(Guid Id, Guid DistrictId, string Name, bool IsNew)
    : ICommand<Guid>;
