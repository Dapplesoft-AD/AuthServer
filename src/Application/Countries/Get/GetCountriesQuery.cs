using Application.Abstractions.Messaging;

namespace Application.Countries.Get;

public sealed record GetCountriesQuery() : IQuery<List<GetCountryResponse>>;
