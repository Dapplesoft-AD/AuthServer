
using Application.Abstractions.Messaging;
using Application.Customers.Create;
using Application.Todos.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customer", async (
            Request request,
            ICommandHandler<CreateSupplierCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateSupplierCommand
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address
            };

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Customers)
        .RequireAuthorization();
    }
}
