using Application.Abstractions.Messaging;
using Application.Application.Create;
using Application.Customers.Create;
using Application.Todos.Create;
using Domain.Application;
using Application.Abstractions;
using Domain.Enums;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Application;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public string Name { get; set; }
        public string Client_id { get; set; } = string.Empty;
        public string Client_secret { get; set; }
        public string Redirect_url { get; set; }
        public string Api_base_url { get; set; }
        public Status Application_status { get; set; }
    }
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("applications", async (Request request, ICommandHandler<CreateApplicationCommand, Guid> handler, CancellationToken cancellationToken) =>
        {
            var command = new CreateApplicationCommand
            {
                Name = request.Name,
                Client_id = request.Client_id,
                Client_secret = request.Client_secret,
                Redirect_url = request.Redirect_url,
                Api_base_url = request.Api_base_url,
                Application_status = request.Application_status
            };
            Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
