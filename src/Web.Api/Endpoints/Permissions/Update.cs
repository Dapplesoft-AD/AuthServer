using Application.Abstractions.Messaging;
using Application.Permissions.Update;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Permissions;

internal sealed class Update : IEndpoint
{
    public sealed record Request(string Code, string Description);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("permissions/{id:guid}", async (
            Guid id,
            Request request,
            ICommandHandler<UpdatePermissionCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new UpdatePermissionCommand(id, request.Code, request.Description);

            Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                permissionId => Results.Ok(new { Id = permissionId, Message = "Permission updated successfully." }),
                CustomResults.Problem);
        })
        .WithTags("Permissions")
        .RequireAuthorization();
    }
}
