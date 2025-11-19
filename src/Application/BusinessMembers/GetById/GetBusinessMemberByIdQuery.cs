using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.BusinessMembers.Get;

namespace Application.BusinessMembers.GetById;
public sealed record GetBusinessMemberByIdQuery(Guid Id) : IQuery<BusinessMemberResponse>;
