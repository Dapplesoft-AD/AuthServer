using Application.Abstractions.Messaging;
using Application.BusinessMembers.Create;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.BusinessMembers;

public class Create : IEndpoint
{
    public sealed class Request
    {
        public Guid BusinessId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/business-members", async (
            Create.Request request,
            ICommandHandler<CreateBusinessMemberCommand, Guid> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateBusinessMemberCommand(
                request.BusinessId,
                request.UserId,
                request.RoleId
            );

            SharedKernel.Result<Guid> result = await handler.Handle(command, cancellationToken);

            return result.Match(
            id => Results.Created($"/business-members/{id}", new { id }),
            CustomResults.Problem
 );
        })

        .WithTags(Tags.BusinessMembers)
        .RequireAuthorization();
    }
}
