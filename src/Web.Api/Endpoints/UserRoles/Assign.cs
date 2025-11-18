using Application.Abstractions.Messaging;
using Application.UserRoles.Assign;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.UserRoles;

internal sealed class Assign : IEndpoint
{
    public sealed record Request(Guid UserId, Guid RoleId, Guid ApplicationId);

    public sealed record Response(string Message, Guid UserId, Guid RoleId, Guid ApplicationId, DateTime AssignedAt);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("user-roles/assign", async (
            Request request,
            ICommandHandler<AssignRoleToUserCommand> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new AssignRoleToUserCommand(
                request.UserId,
                request.RoleId,
                request.ApplicationId
            );

            Result result = await handler.Handle(command, cancellationToken);

            return result.Match(
                () => Results.Ok(new Response(
                    Message: "Role assigned to user successfully",
                    UserId: request.UserId,
                    RoleId: request.RoleId,
                    ApplicationId: request.ApplicationId,
                    AssignedAt: DateTime.UtcNow
                )),
                CustomResults.Problem);
        })
        .WithTags("UserRoles")
        .RequireAuthorization()
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}
