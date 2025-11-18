using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Applications.Get;
public sealed record GetApplicationByIdQuery(Guid Id) : IQuery<ApplicationResponse>;
