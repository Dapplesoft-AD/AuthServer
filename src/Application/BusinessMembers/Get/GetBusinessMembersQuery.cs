using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Get;

public sealed record GetBusinessMembersQuery() : IQuery<List<BusinessMemberResponse>>
{

}
