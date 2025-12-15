using Application.Abstractions.Messaging;
using Application.Projects.Create;
using Domain.Projects;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Projects;

internal sealed class Create : IEndpoint
{
    public sealed record Request(
        string Name,
        string ClientId,
        string Domain,
        string ClientSecret,
        string RedirectUri,
        int Status 
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.Projects), async (
            Request request,
            ICommandHandler<CreateProjectCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            if (!Enum.IsDefined(typeof(ProjectStatus), request.Status))
            {
                return Results.BadRequest("Invalid status value. Use 1 for Active or 2 for Inactive.");
            }

            var command = new CreateProjectCommand(
                Name: request.Name,
                ClientId: request.ClientId,
                Domain: request.Domain,
                ClientSecret: request.ClientSecret,
                RedirectUrl: request.RedirectUri,
                Status: (ProjectStatus) request.Status
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Projects)
        .RequireAuthorization();
    }
}
