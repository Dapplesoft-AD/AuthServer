using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Businesses.Delete;
public sealed record DeleteBusinessCommand(Guid Id) : ICommand<Guid>;
