using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Get;
public sealed record GetBusinessMembersQuery() : IQuery<List<BusinessMemberResponse>>
{

}
