using Application.Abstractions.Messaging;
using Application.Permissions.Create;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Permissions;

internal sealed class Create : IEndpoint
{
    public sealed record Request(string Code, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("permissions", async (
            Request request,
            ICommandHandler<CreatePermissionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreatePermissionCommand(
                request.Code,
                request.Description
            );

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                id => Results.Ok(new { Id = id, Message = "Permission created successfully" }),
                CustomResults.Problem
            );
        })
        .WithTags("Permissions")
        .RequireAuthorization();
    }
}
