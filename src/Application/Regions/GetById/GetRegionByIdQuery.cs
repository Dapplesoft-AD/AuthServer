using System;
using Application.Abstractions.Messaging;
using Application.Regions.Get;

namespace Application.Regions.GetById;

public sealed record GetRegionByIdQuery(Guid Id)
    : IQuery<RegionResponse>;
