using Application.Abstractions.Messaging;

namespace Application.Localities.Get;

public sealed record GetLocalityByIdQuery(Guid Id) : IQuery<LocalityResponse>;
