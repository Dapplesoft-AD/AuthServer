using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Application.Applications.Get;

namespace Application.Applications.GetAll;
public sealed record GetAllApplicationsQuery() : IQuery<List<ApplicationResponse>>;
