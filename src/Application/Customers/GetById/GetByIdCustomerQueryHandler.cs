using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Todos;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Customers.GetById;
internal sealed class GetByIdCustomerQueryHandler(IApplicationDbContext context) : IQueryHandler<GetByIdCustomerQuery, GetCustomerResponse>
{
    public async Task<Result<GetCustomerResponse>> Handle(GetByIdCustomerQuery query, CancellationToken cancellationToken)
    {
        GetCustomerResponse? getCustomer = await context.Customers
            .Where(c => c.Id == query.CustomerId)
            .Select(c => new GetCustomerResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (getCustomer is null)
        {
            return Result.Failure<GetCustomerResponse>(TodoItemErrors.NotFound(query.CustomerId));
        }

        return getCustomer;
    }
}
