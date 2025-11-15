
using System.Reflection.Metadata;
using Application.Abstractions.Messaging;
using Application.Customers.Delete;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("customer/{id:guid}", async(
            Guid id,
            ICommandHandler<DeleteCustomerCommand> handler,
            CancellationToken cancellationToken
            ) => {
                var command = new DeleteCustomerCommand(id);

                Result result = await handler.Handle(command, cancellationToken);

                result.Match(Results.NoContent, CustomResults.Problem);
            })
            .WithTags(Tags.Customers);
    }
}
