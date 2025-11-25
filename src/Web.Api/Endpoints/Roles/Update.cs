using Application.Abstractions.Messaging;
using Application.Roles.Update;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Roles;

internal sealed class Update : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("roles/{id:guid}", async (
            Guid id,
            UpdateRoleCommand command,
            ICommandHandler<UpdateRoleCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            UpdateRoleCommand updatedCommand = command with { Id = id };
            SharedKernel.Result<Guid> result = await handler.Handle(updatedCommand, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Roles);
    }
}
