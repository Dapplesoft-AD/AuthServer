using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Permissions.Delete;
public sealed record DeletePermissionCommand(Guid Id) : ICommand;
