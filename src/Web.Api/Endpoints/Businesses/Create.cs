using Application.Abstractions.Messaging;
using Application.Businesses.Create;
using Domain.Businesses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Businesses;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid OwnerUserId { get; set; }
        public string BusinessName { get; set; }
        public string IndustryType { get; set; }
        public string LogoUrl { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {

        app.MapPost("/businesses", async (
            Request request,
            ICommandHandler<CreateBusinessCommand, Guid> handler,
             HttpContext httpContext,
            CancellationToken cancellationToken) =>

        {

            var command = new CreateBusinessCommand

            {
                OwnerUserId = request.OwnerUserId,
                BusinessName = request.BusinessName,
                IndustryType = request.IndustryType,
                LogoUrl = request.LogoUrl,
                Status = request.Status,

            };

            // Handle command using CQRS handler
            Result<Guid> result = await handler.Handle(command, cancellationToken);

            // Return HTTP 200 OK if success, ProblemDetails if failure
            return result.Match(Results.Ok, CustomResults.Problem);

        })
        .WithTags(Tags.Businesses)
        .RequireAuthorization();
    }
}
