using Application.Abstractions.Messaging;

namespace Application.Businesses.GetById;

public sealed record GetBusinessByIdQuery(Guid Id) : IQuery<GetBusinessByIdResponse>;
