using Application.Abstractions.Messaging;

namespace Application.Regions.Get;

public sealed record GetRegionQuery()
    : IQuery<List<RegionResponse>>;
