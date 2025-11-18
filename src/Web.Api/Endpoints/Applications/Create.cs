using Application.Abstractions.Messaging;
using Application.Applications.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Applications;

internal sealed class Create : IEndpoint
{
    public sealed record Request(
        string Name,
        string ClientId,
        string ClientSecret,
        string RedirectUri,
        string ApiBaseUrl,
        int Status // Use int to match enum storage, or create a string enum mapping
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("applications", async (
            Request request,
            ICommandHandler<CreateApplicationCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            // Validate and convert Status to enum
            if (!Enum.IsDefined(typeof(Domain.Applications.Applicationapply.ApplicationStatus), request.Status))
            {
                return Results.BadRequest("Invalid status value. Use 1 for Active or 2 for Inactive.");
            }

            var status = (Domain.Applications.Applicationapply.ApplicationStatus)request.Status;

            var command = new CreateApplicationCommand(
                request.Name,
                request.ClientId,
                request.ClientSecret,
                new Uri(request.RedirectUri), // ✅ convert string to Uri
    new Uri(request.ApiBaseUrl),  // Convert string to Uri
                status
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
