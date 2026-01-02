using System;
using Application.Abstractions.Messaging;

namespace Application.SubDistricts.Get;

public sealed record GetSubDistrictByIdQuery(Guid Id) : IQuery<SubDistrictResponse>;
