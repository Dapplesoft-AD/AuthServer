using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.BusinessMembers.Delete;
public sealed record DeleteBusinessMemberCommand(Guid Id) : ICommand<Guid>;
