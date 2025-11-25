using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.GetById;
public sealed record GetBusinessMemberByIdQuery(Guid Id) : IQuery<BusinessMemberResponse>;
