using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Applications.Delete;
public sealed record DeleteApplicationCommand(Guid Id) : ICommand;
