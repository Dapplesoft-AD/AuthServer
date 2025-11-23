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
        int Status 
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("applications", async (
            Request request,
            ICommandHandler<CreateApplicationCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            if (!Enum.IsDefined(typeof(Domain.Applications.Applicationapply.ApplicationStatus), request.Status))
            {
                return Results.BadRequest("Invalid status value. Use 1 for Active or 2 for Inactive.");
            }

            var status = (Domain.Applications.Applicationapply.ApplicationStatus)request.Status;

            var command = new CreateApplicationCommand(
                request.Name,
                request.ClientId,
                request.ClientSecret,
                new Uri(request.RedirectUri), 
                new Uri(request.ApiBaseUrl), 
                status
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags("Applications")
        .RequireAuthorization();
    }
}
