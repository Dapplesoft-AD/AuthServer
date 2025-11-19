using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Create;
public sealed record CreateBusinessMemberCommand(
    Guid BusinessId,
    Guid UserId,
    Guid RoleId
) : ICommand<Guid>;
