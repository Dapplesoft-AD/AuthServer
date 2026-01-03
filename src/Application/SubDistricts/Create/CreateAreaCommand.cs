using Application.Abstractions.Messaging;
using Domain.SubDistricts;

namespace Application.SubDistricts.Create;

public sealed record CreateAreaCommand(
    Guid CountryId,
    Guid DistrictId,
    string Name,
    bool IsNew
) : ICommand<Guid>;
