using Application.Abstractions.Messaging;
using Application.BusinessMembers.Create;
using Application.ClientApps.Create;
using SharedKernel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web.Api.Endpoints.ClientApps;

public sealed class Create : IEndpoint
{
    public sealed record Request
    (
        string ClientId,
        string DisplayName,
        string ClientSecret,
        Uri[] RedirectUris
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Create(Base.ClientApps), async (
            Request request,
            ICommandHandler<CreateClientCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateClientCommand(
                request.ClientId,
                request.DisplayName,
                request.ClientSecret,
                request.RedirectUris
            );
            Result result = await handler.Handle(command, cancellationToken);
            return result.IsSuccess
                ? Results.Created($"{request.ClientId}", null)
                : Results.BadRequest(result.Error);
        })
        .WithTags(Tags.ClientApps)
        .RequireAuthorization();
    }
}
