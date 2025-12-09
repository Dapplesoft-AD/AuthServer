using Application.Abstractions.Messaging;

namespace Application.Districts.Get;

public sealed record GetDistrictQuery()
    : IQuery<List<DistrictResponse>>;
