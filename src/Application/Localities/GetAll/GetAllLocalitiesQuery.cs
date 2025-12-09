using Application.Abstractions.Messaging;
using Application.Localities.Get;

namespace Application.Localities.GetAll;

public sealed record GetAllLocalitiesQuery() : IQuery<List<LocalityResponse>>;
