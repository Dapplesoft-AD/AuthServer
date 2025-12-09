using Application.Abstractions.Messaging;

namespace Application.Countries.GetById;

public sealed record GetCountryByIdQuery(Guid Id) : IQuery<GetCountryByIdResponse>;
