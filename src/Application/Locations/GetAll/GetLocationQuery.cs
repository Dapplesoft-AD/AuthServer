using Application.Abstractions.Messaging;

namespace Application.Locations.GetAll;

public sealed class GetLocationQuery : IQuery<List<GetLocationQueryResponse>>;
