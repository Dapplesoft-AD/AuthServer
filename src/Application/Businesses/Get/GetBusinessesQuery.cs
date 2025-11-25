using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Businesses.Get;

public sealed record GetBusinessesQuery() : IQuery<List<GetBusinessResponse>>;
