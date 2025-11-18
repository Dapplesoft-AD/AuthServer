using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.UserRoles.Assign;
public sealed record AssignRoleToUserCommand(Guid UserId, Guid RoleId, Guid ApplicationId) : ICommand;
