using System;
using Application.Abstractions.Messaging;
using Application.Districts.Get;

namespace Application.Districts.GetById;

public sealed record GetDistrictByIdQuery(Guid Id)
    : IQuery<DistrictResponse>;
