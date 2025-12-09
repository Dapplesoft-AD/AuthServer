using System;
using Application.Abstractions.Messaging;

namespace Application.Areas.Get;

public sealed record GetAreaByIdQuery(Guid Id) : IQuery<AreaResponse>;
