
using Application.Abstractions.Messaging;
using Application.Customers.GetById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customer/{id:guid}", async (
            Guid Id,
            IQueryHandler<GetByIdCustomerQuery, GetCustomerResponse> handler,
            CancellationToken cancellationToken) => {
                var query = new GetByIdCustomerQuery(Id);

                Result<GetCustomerResponse> result = await handler.Handle(query, cancellationToken);

                return result.Match(Results.Ok, CustomResults.Problem);
            }).WithTags(Tags.Customers);
    }
}
