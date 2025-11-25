using Application.Abstractions.Messaging;
using Application.Roles.Create;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Roles;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("roles", async (
            CreateRoleCommand request,
            ICommandHandler<CreateRoleCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateRoleCommand(
                request.RoleName,
                request.Description
            );

            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);
            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Roles);
    }
}
