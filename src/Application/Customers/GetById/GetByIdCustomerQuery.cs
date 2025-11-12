using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;

namespace Application.Customers.GetById;
public sealed record GetByIdCustomerQuery(Guid CustomerId) : IQuery<GetCustomerResponse>;
