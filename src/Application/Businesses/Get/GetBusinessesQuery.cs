using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Businesses.Get;
public sealed record GetBusinessesQuery(Guid OwnerUserId) : IQuery<List<BusinessResponse>>;
