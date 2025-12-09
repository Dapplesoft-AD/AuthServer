using Application.Abstractions.Messaging;
using Application.Areas.Get;

namespace Application.Areas.GetAl;

public sealed record GetAllAreasQuery() : IQuery<List<AreaResponse>>;
